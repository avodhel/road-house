using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPath : MonoBehaviour
{
    public Transform treePoint;
    public Transform buildingPoint;
    public GameObject[] trees;
    public GameObject[] buildings;

    [Header("AI Car")]
    public GameObject aiCar;
    public Transform aiCarSpawnPoint;

    private void Start()
    {
        PrepareEnvironment(trees, treePoint);
        PrepareEnvironment(buildings, buildingPoint);
        SpawnAICar();
    }

    private void SpawnAICar()
    {
        Vector3 aiCarRot = aiCar.GetComponent<AICar>().StartedRotation(gameObject.transform.rotation.eulerAngles.y);
        Instantiate(aiCar, aiCarSpawnPoint.transform.position, Quaternion.Euler(aiCarRot));
    }

    private void PrepareEnvironment(GameObject[] environmentObjects, Transform objectPoint)
    {
        int instantiatePossibility = Random.Range(0, 100);
        if (instantiatePossibility < 50)
        {
            GameObject choosenObject = environmentObjects[Random.Range(0, environmentObjects.Length)];

            var objectRotation = new Vector3(choosenObject.transform.rotation.eulerAngles.x,
                                             gameObject.transform.rotation.eulerAngles.y + choosenObject.transform.rotation.eulerAngles.y,
                                             choosenObject.transform.rotation.eulerAngles.z);

            GameObject instantiatedObject = Instantiate(choosenObject, objectPoint.position, Quaternion.Euler(objectRotation));
            (instantiatedObject as GameObject).transform.parent = objectPoint.transform;
        }
        else
        {
            return;
        }
    }
}
