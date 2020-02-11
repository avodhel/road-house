using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePath : MonoBehaviour
{
    public GameObject[] sidePlaces;
    public PathDirection mergePathDir;

    public delegate void PrepareMergePath(PathDirection dir);
    public PrepareMergePath prepareMergePath;

    private GameObject instantiatedPlace;

    private void Awake()
    {
        prepareMergePath += ChooseAndSpawnSidePlace;
        prepareMergePath += MarkingMergePath;
        prepareMergePath += FencePlacing;
    }

    private void ChooseAndSpawnSidePlace(PathDirection dir)
    {
        GameObject choosenPlace = sidePlaces[Random.Range(0, sidePlaces.Length)];

        if (dir == PathDirection.Top)
        {
            instantiatedPlace =  Instantiate(choosenPlace, 
                                             gameObject.transform.GetChild(0).transform.GetChild(2).transform.position, 
                                             Quaternion.identity);
        }

        else if (dir == PathDirection.Left)
        {
            Vector3 objectRotation = new Vector3(choosenPlace.transform.rotation.eulerAngles.x,
                                                 choosenPlace.transform.rotation.eulerAngles.y + 90,
                                                 choosenPlace.transform.rotation.eulerAngles.z);

            instantiatedPlace = Instantiate(choosenPlace,
                                            gameObject.transform.GetChild(0).transform.GetChild(2).transform.position,
                                            Quaternion.Euler(objectRotation));
        }

        else if (dir == PathDirection.Right)
        {
            Vector3 objectRotation = new Vector3(choosenPlace.transform.rotation.eulerAngles.x,
                                                 choosenPlace.transform.rotation.eulerAngles.y - 90,
                                                 choosenPlace.transform.rotation.eulerAngles.z);

            instantiatedPlace = Instantiate(choosenPlace,
                                            gameObject.transform.GetChild(0).transform.GetChild(0).transform.position,
                                            Quaternion.Euler(objectRotation));           
        }

        (instantiatedPlace as GameObject).transform.parent = gameObject.transform;
    }

    private void MarkingMergePath(PathDirection dir)
    {
        gameObject.transform.GetChild(1).transform.GetChild((int)dir).gameObject.SetActive(true);
    }

    private void FencePlacing(PathDirection dir)
    {
        gameObject.transform.GetChild(3).transform.GetChild((int)dir).gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "carTag")
        {
            Spawn.SpawnManager.MultiplePathsSpawner(5);
        }
    }
}
