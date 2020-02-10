using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPath : MonoBehaviour
{
    [Header("Tree")]
    public Transform treePoint;
    public GameObject[] trees;

    [Header("Building")]
    public Transform buildingPoint;
    public GameObject[] buildings;

    [Header("AI Car")]
    public Transform aiCarSpawnPoint;

    private void Start()
    {
        PrepareEnvironment(trees, treePoint);
        PrepareEnvironment(buildings, buildingPoint);
        Spawn.SpawnManager.SpawnAICar(aiCarSpawnPoint, gameObject.transform.rotation.eulerAngles.y);
    }

    private void PrepareEnvironment(GameObject[] environmentObjects, Transform objectPoint)
    {
        //int instantiatePossibility = Random.Range(0, 100);
        //if (instantiatePossibility < 50)
        //{
            GameObject choosenObject = environmentObjects[Random.Range(0, environmentObjects.Length)];

            var objectRotation = new Vector3(choosenObject.transform.rotation.eulerAngles.x,
                                             gameObject.transform.rotation.eulerAngles.y + choosenObject.transform.rotation.eulerAngles.y,
                                             choosenObject.transform.rotation.eulerAngles.z);

            GameObject instantiatedObject = Instantiate(choosenObject, objectPoint.position, Quaternion.Euler(objectRotation));
            (instantiatedObject as GameObject).transform.parent = objectPoint.transform;
        //}
        //else
        //{
        //    return;
        //}
    }
}
