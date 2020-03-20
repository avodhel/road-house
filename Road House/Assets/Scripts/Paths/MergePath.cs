using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePath : Paths
{
    public GameObject placeContainer;
    public GameObject[] sidePlaces;
    public PathDirection mergePathDir;

    public delegate void PrepareMergePath(PathDirection dir);
    public PrepareMergePath prepareMergePath;

    private GameObject instantiatedPlace;

    private void Awake()
    {
        InstantiatePlace();
        prepareMergePath += ChooseAndSpawnSidePlace;
        prepareMergePath += MarkingMergePath;
        prepareMergePath += FencePlacing;
    }

    private void OnDisable()
    {
        for (int i = 0; i < placeContainer.transform.childCount; i++)
        {
            placeContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        //markings
        for (int i = 0; i < gameObject.transform.GetChild(1).transform.childCount; i++)
        {
            if (i != 3)
            {
                gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        //fences
        for (int i = 0; i < gameObject.transform.GetChild(3).transform.childCount; i++)
        {
            gameObject.transform.GetChild(3).transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void InstantiatePlace()
    {
        for (int i = 0; i < sidePlaces.Length; i++)
        {
            GameObject instantiatedSidePlace = Instantiate(sidePlaces[i], gameObject.transform.position, Quaternion.identity);
            (instantiatedSidePlace as GameObject).transform.parent = placeContainer.transform;
            instantiatedSidePlace.SetActive(false);
        }
    }

    private void ChooseAndSpawnSidePlace(PathDirection dir)
    {
        //GameObject choosenPlace = sidePlaces[Random.Range(0, sidePlaces.Length)];
        Transform choosenPlace = placeContainer.transform.GetChild(Random.Range(0, placeContainer.transform.childCount));
        choosenPlace.rotation = Quaternion.Euler(Vector3.zero);

        if (dir == PathDirection.Top)
        {
            //instantiatedPlace =  Instantiate(choosenPlace, 
            //                                 gameObject.transform.GetChild(0).transform.GetChild(2).transform.position, 
            //                                 Quaternion.identity);
            choosenPlace.position = gameObject.transform.GetChild(0).transform.GetChild(2).transform.position;
            choosenPlace.rotation = Quaternion.identity;
        }

        else if (dir == PathDirection.Left)
        {
            Vector3 objectRotation = new Vector3(choosenPlace.transform.rotation.eulerAngles.x,
                                                 choosenPlace.transform.rotation.eulerAngles.y + 90,
                                                 choosenPlace.transform.rotation.eulerAngles.z);

            //instantiatedPlace = Instantiate(choosenPlace,
            //                                gameObject.transform.GetChild(0).transform.GetChild(2).transform.position,
            //                                Quaternion.Euler(objectRotation));
            choosenPlace.position = gameObject.transform.GetChild(0).transform.GetChild(2).transform.position;
            choosenPlace.rotation = Quaternion.Euler(objectRotation);
        }

        else if (dir == PathDirection.Right)
        {
            Vector3 objectRotation = new Vector3(choosenPlace.transform.rotation.eulerAngles.x,
                                                 choosenPlace.transform.rotation.eulerAngles.y - 90,
                                                 choosenPlace.transform.rotation.eulerAngles.z);

            //instantiatedPlace = Instantiate(choosenPlace,
            //                                gameObject.transform.GetChild(0).transform.GetChild(0).transform.position,
            //                                Quaternion.Euler(objectRotation));      
            choosenPlace.position = gameObject.transform.GetChild(0).transform.GetChild(0).transform.position;
            choosenPlace.rotation = Quaternion.Euler(objectRotation);
        }

        //(instantiatedPlace as GameObject).transform.parent = gameObject.transform;
        choosenPlace.gameObject.SetActive(true);
    }

    private void MarkingMergePath(PathDirection dir)
    {
        gameObject.transform.GetChild(1).transform.GetChild((int)dir).gameObject.SetActive(true);
    }

    private void FencePlacing(PathDirection dir)
    {
        gameObject.transform.GetChild(3).transform.GetChild((int)dir).gameObject.SetActive(false);
    }
}
