using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Path")]
    public GameObject currentPath;
    public GameObject normalPath;
    public GameObject[] paths;

    [Header("AI Car")]
    public GameObject aiCar;

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
        MultiplePathsSpawner(15);
    }

    public void SpawnAICar(Transform carSpawnPoint, float carRotation)
    {
        Vector3 aiCarRot = aiCar.GetComponent<AICar>().StartedRotation(carRotation);
        Instantiate(aiCar, carSpawnPoint.transform.position, Quaternion.Euler(aiCarRot));
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
        currentPath = Instantiate(paths[Random.Range(0, paths.Length)],
                                  currentPath.transform.GetChild(1).transform.position,
                                  currentPath.transform.rotation);
    }

    private void SpawnNormalPath()
    {
        Vector3 pathRotation = GetPathRotationAccordingToDir();

        currentPath = Instantiate(normalPath,
                          currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                          Quaternion.Euler(pathRotation));
    }

    private Vector3 GetPathRotationAccordingToDir()
    {
        Vector3 rotationVec3 = Vector3.zero;
        float rotationY = currentPath.transform.rotation.eulerAngles.y;

        if (ChoosePathDirection() == PathDirection.Top)
        {
            Debug.Log("Top");
            currentDir = PathDirection.Top;
            rotationVec3 = currentPath.transform.rotation.eulerAngles;
        }
        else if (ChoosePathDirection() == PathDirection.Left)
        {
            Debug.Log("Left");
            currentDir = PathDirection.Left;
            rotationVec3 = transform.rotation.eulerAngles;
            rotationVec3.y = rotationY - 90;
        }
        else if (ChoosePathDirection() == PathDirection.Right)
        {
            Debug.Log("Right");
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
