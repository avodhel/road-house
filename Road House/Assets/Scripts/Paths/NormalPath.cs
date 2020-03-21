using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPath : Paths
{
    [Header("Tree")]
    public GameObject treeContainer;
    public Transform treePoint;
    public GameObject[] trees;

    [Header("Building")]
    public GameObject buildingContainer;
    public Transform buildingPoint;
    public GameObject[] buildings;

    [Header("Fences")]
    public GameObject fences;

    [Header("AI Car")]
    public Transform aiCarSpawnPoint;

    [Header("Coin")]
    public Transform coinSpawnPoint;

    private void Awake()
    {
        InstantiateEnvironment(trees, treePoint, treeContainer);
        InstantiateEnvironment(buildings, buildingPoint, buildingContainer);
    }

    private void OnEnable()
    {
        ChooseObjectFromContainer(treeContainer);
        ChooseObjectFromContainer(buildingContainer);
    }

    private void OnDisable()
    {
        for (int i = 0; i < fences.transform.childCount; i++)
        {
            fences.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < treeContainer.transform.childCount; i++)
        {
            treeContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < buildingContainer.transform.childCount; i++)
        {
            buildingContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SpawnObject()
    {
        int spawnPossibility = Random.Range(0, 100);
        if (spawnPossibility < 60)
        {
            Spawn.SpawnManager.SpawnAICar(aiCarSpawnPoint, gameObject.transform.rotation.eulerAngles.y);
        }
        else if (spawnPossibility > 60 && spawnPossibility < 80)
        {
            Spawn.SpawnManager.SpawnCoin(coinSpawnPoint);
        }
        else
        {
            return;
        }
    }

    private void InstantiateEnvironment(GameObject[] environmentObjects, Transform objectPoint, GameObject container)
    {
        for (int i = 0; i < environmentObjects.Length; i++)
        {
            var objectRotation = new Vector3(environmentObjects[i].transform.rotation.eulerAngles.x,
                                 gameObject.transform.rotation.eulerAngles.y + environmentObjects[i].transform.rotation.eulerAngles.y,
                                 environmentObjects[i].transform.rotation.eulerAngles.z);

            GameObject instantiatedObject = Instantiate(environmentObjects[i], objectPoint.position, Quaternion.Euler(objectRotation));
            (instantiatedObject as GameObject).transform.parent = container.transform;
            instantiatedObject.SetActive(false);
        }
    }

    private void ChooseObjectFromContainer(GameObject container)
    {
        container.transform.GetChild(Random.Range(0, container.transform.childCount)).gameObject.SetActive(true);
    }
}
