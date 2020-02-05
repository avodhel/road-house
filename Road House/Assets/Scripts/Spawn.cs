using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject currentPath;
    //public GameObject mergePath;
    public GameObject normalPath;
    public GameObject[] paths;

    private enum Direction
    {
        Top = 0,
        Left = 1,
        Right = 2
    }
    private static Direction currentDir;

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
        for (int i = 0; i < 10; i++)
        {
            SpawnPath();
        }
    }

    public void SpawnPath()
    {
        if (currentPath.tag == "mergePathTag")
        {
            float rotationY = currentPath.transform.rotation.eulerAngles.y;

            if (ChoosePathDirection() == Direction.Top)
            {
                Debug.Log("Top");
                currentDir = Direction.Top;
                currentPath = Instantiate(normalPath,
                                          currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                                          currentPath.transform.rotation);
            }
            else if (ChoosePathDirection() == Direction.Left)
            {
                currentDir = Direction.Left;
                Debug.Log("Left");
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = rotationY - 90;
                currentPath = Instantiate(normalPath,
                                          currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                                          Quaternion.Euler(rotationVector));
            }
            else if (ChoosePathDirection() == Direction.Right)
            {
                currentDir = Direction.Right;
                Debug.Log("Right");
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = rotationY + 90;
                currentPath = Instantiate(normalPath,
                                          currentPath.transform.GetChild(0).transform.GetChild((int)currentDir).transform.position,
                                          Quaternion.Euler(rotationVector));
            }
        }
        else if (currentPath.tag == "normalPathTag")
        {
            //Debug.Log("Top");
            currentPath = Instantiate(paths[Random.Range(0, paths.Length)], 
                                      currentPath.transform.GetChild(1).transform.position, 
                                      currentPath.transform.rotation);
        }

    }

    private static Direction ChoosePathDirection()
    {
        //int randInt = Random.Range(0, 100);
        if (currentDir == Direction.Left)
        {
            //randDirIndex = randInt < 10 ? 0 : 2;
            //randDirIndex = 2;
            return Direction.Right;
        }
        else if (currentDir == Direction.Right)
        {
            //randDirIndex = randInt < 10 ? 0 : 1;
            //randDirIndex = Random.Range(0, 2);
            //randDirIndex = 1;
            return Direction.Left;
        }
        else
        {
            //randDirIndex = Random.Range(0, 3);
            Direction[] validDirections = new[] { Direction.Top, Direction.Left, Direction.Right };
            Direction selectedDirection = validDirections[Random.Range(0, validDirections.Length)];
            return selectedDirection;
        }
    }
}
