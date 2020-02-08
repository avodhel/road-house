using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePath : MonoBehaviour
{
    public GameObject[] sidePlaces;
    public PathDirection mergePathDir;

    public delegate void PrepareMergePath(PathDirection dir);
    public PrepareMergePath prepareMergePath;

    private void Awake()
    {
        prepareMergePath += ChoosingSidePlace;
        prepareMergePath += MarkingMergePath;
        prepareMergePath += FencePlacing;
    }

    private void ChoosingSidePlace(PathDirection dir)
    {
        GameObject choosenPlace = sidePlaces[Random.Range(0, sidePlaces.Length)];
        if (dir == PathDirection.Top)
        {
            GameObject instantiatedTrafficLight =  Instantiate(choosenPlace, 
                                                               gameObject.transform.GetChild(0).transform.GetChild(2).transform.position, 
                                                               Quaternion.identity);
            (instantiatedTrafficLight as GameObject).transform.parent = gameObject.transform;
        }

        else if (dir == PathDirection.Left)
        {
            var objectRotation = new Vector3(choosenPlace.transform.rotation.eulerAngles.x,
                                             choosenPlace.transform.rotation.eulerAngles.y + 90,
                                             choosenPlace.transform.rotation.eulerAngles.z);

            GameObject instantiatedTrafficLight = Instantiate(choosenPlace,
                                                               gameObject.transform.GetChild(0).transform.GetChild(2).transform.position,
                                                               Quaternion.Euler(objectRotation));
            (instantiatedTrafficLight as GameObject).transform.parent = gameObject.transform;
        }

        else if (dir == PathDirection.Right)
        {
            var objectRotation = new Vector3(choosenPlace.transform.rotation.eulerAngles.x,
                                             choosenPlace.transform.rotation.eulerAngles.y - 90,
                                             choosenPlace.transform.rotation.eulerAngles.z);

            GameObject instantiatedTrafficLight = Instantiate(choosenPlace,
                                                               gameObject.transform.GetChild(0).transform.GetChild(0).transform.position,
                                                               Quaternion.Euler(objectRotation));
            (instantiatedTrafficLight as GameObject).transform.parent = gameObject.transform;
        }
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
