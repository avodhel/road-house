using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Path")]
    public int numberOfPath = 5;
    public GameObject currentPath;
    public GameObject normalPath;
    public GameObject[] paths;

    [Header("AI Car")]
    public GameObject aiCar;

    [Header("Coin")]
    public GameObject goldCoin;

    public static Spawn SpawnManager { get; private set; }

    private static PathDirection currentDir;

    private void Awake()
    {
        if (SpawnManager == null)
        {
            SpawnManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentDir = PathDirection.Top;
        MultiplePathsSpawner(numberOfPath);
    }

    public void SpawnAICar(Transform carSpawnPoint, float carRotation)
    {
        GameObject instantiatedAICar = AICarPooler.SharedInstance.GetPooledObject(0);
        Vector3 aiCarRot = instantiatedAICar.GetComponent<AICar>().StartedRotation(carRotation);
        instantiatedAICar.transform.position = carSpawnPoint.transform.position;
        instantiatedAICar.transform.rotation = Quaternion.Euler(aiCarRot);
        instantiatedAICar.SetActive(true);
    }

    public void SpawnCoin(Transform coinSpawnPoint)
    {
        GameObject instantiatedCoin = CoinPooler.SharedInstance.GetPooledObject(0);
        instantiatedCoin.transform.position = coinSpawnPoint.transform.position;
        instantiatedCoin.SetActive(true);
    }

    public void MultiplePathsSpawner(int numberOfPaths)
    {
        for (int i = 0; i < numberOfPaths; i++)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        if (currentPath.tag == "mergePathTag")
        {
            SpawnNormalPath();
        }
        else if (currentPath.tag == "normalPathTag")
        {
            SpawnRandomPath();
        }

    }

    private void SpawnRandomPath()
    {
        GameObject instantiatedRandomPath = PathPooler.SharedInstance.GetPooledObject(Random.Range(0, PathPooler.SharedInstance.itemsToPool.Count));
        instantiatedRandomPath.SetActive(true);
        instantiatedRandomPath.transform.position = currentPath.transform.GetChild(1).transform.position;
        instantiatedRandomPath.transform.rotation = currentPath.transform.rotation;
        currentPath = instantiatedRandomPath;
        if (instantiatedRandomPath.transform.tag == "normalPathTag")
        {
            instantiatedRandomPath.GetComponent<NormalPath>().SpawnObject();
        }
    }

    private void SpawnNormalPath()
    {
        Vector3 pathRotation = GetPathRotationAccordingToDir();

        GameObject instantiatedNormalPath = PathPooler.SharedInstance.GetPooledObject(1);
        instantiatedNormalPath.SetActive(true);
        instantiatedNormalPath.transform.position = currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position;
        instantiatedNormalPath.transform.rotation = Quaternion.Euler(pathRotation);
        currentPath = instantiatedNormalPath;
        instantiatedNormalPath.GetComponent<NormalPath>().SpawnObject();
    }

    private Vector3 GetPathRotationAccordingToDir()
    {
        Vector3 rotationVec3 = Vector3.zero;
        float rotationY = currentPath.transform.rotation.eulerAngles.y;

        if (ChoosePathDirection() == PathDirection.Top)
        {
            currentDir = PathDirection.Top;
            rotationVec3 = currentPath.transform.rotation.eulerAngles;
        }
        else if (ChoosePathDirection() == PathDirection.Left)
        {
            currentDir = PathDirection.Left;
            rotationVec3 = transform.rotation.eulerAngles;
            rotationVec3.y = rotationY - 90;
        }
        else if (ChoosePathDirection() == PathDirection.Right)
        {
            currentDir = PathDirection.Right;
            rotationVec3 = transform.rotation.eulerAngles;
            rotationVec3.y = rotationY + 90;
        }

        PrepareMergePathForSpawn();

        return rotationVec3;
    }

    private void PrepareMergePathForSpawn()
    {
        currentPath.GetComponent<MergePath>().mergePathDir = currentDir;
        currentPath.GetComponent<MergePath>().prepareMergePath(currentDir);
    }

    private static PathDirection ChoosePathDirection()
    {
        if (currentDir == PathDirection.Left)
        {
            return PathDirection.Right;
        }
        else if (currentDir == PathDirection.Right)
        {
            return PathDirection.Left;
        }
        else
        {
            PathDirection[] validDirections = new[] { PathDirection.Top, PathDirection.Left };
            PathDirection selectedDirection = validDirections[Random.Range(0, validDirections.Length)];
            return selectedDirection;
        }
    }
}
