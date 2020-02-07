using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject currentPath;
    public GameObject normalPath;
    public GameObject[] paths;

    private static PathDirection currentDir;

    public static Spawn SpawnManager { get; private set; }

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
        //Debug.Log("current direction:" + currentDir);
        MultiplePathsSpawner(25);
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
        //if (currentPath.tag == "mergePathTag")
        //{
            //currentPath.GetComponent<MergePath>().PlacingTrafficLight(currentDir);
        //}
    }

    private void SpawnNormalPath()
    {
        float rotationY = currentPath.transform.rotation.eulerAngles.y;

        if (ChoosePathDirection() == PathDirection.Top)
        {
            Debug.Log("Top");
            currentDir = PathDirection.Top;
            currentPath.GetComponent<MergePath>().mergePathDir = currentDir;
            currentPath.GetComponent<MergePath>().MarkingMergePath(currentDir);
            currentPath.GetComponent<MergePath>().ChoosingSidePlace(currentDir);
            currentPath = Instantiate(normalPath,
                                      currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                                      currentPath.transform.rotation);
        }
        else if (ChoosePathDirection() == PathDirection.Left)
        {
            Debug.Log("Left");
            currentDir = PathDirection.Left;
            currentPath.GetComponent<MergePath>().mergePathDir = currentDir;
            currentPath.GetComponent<MergePath>().MarkingMergePath(currentDir);
            currentPath.GetComponent<MergePath>().ChoosingSidePlace(currentDir);
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = rotationY - 90;
            currentPath = Instantiate(normalPath,
                                      currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                                      Quaternion.Euler(rotationVector));
        }
        else if (ChoosePathDirection() == PathDirection.Right)
        {
            Debug.Log("Right");
            currentDir = PathDirection.Right;
            currentPath.GetComponent<MergePath>().mergePathDir = currentDir;
            currentPath.GetComponent<MergePath>().MarkingMergePath(currentDir);
            currentPath.GetComponent<MergePath>().ChoosingSidePlace(currentDir);
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = rotationY + 90;
            currentPath = Instantiate(normalPath,
                                      currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                                      Quaternion.Euler(rotationVector));
        }
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
