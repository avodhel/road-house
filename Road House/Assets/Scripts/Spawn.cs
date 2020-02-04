using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject currentPath;
    public GameObject mergePath;
    public GameObject normalPath;
    public GameObject[] paths;

    private int randDirIndex;
    private enum Direction
    {
        Top = 0,
        Left = 1,
        Right = 2
    }
    private Direction currentDir;

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        if (currentPath.tag == "mergePathTag")
        {
            ChoosePathDirection();

            float rotationY = currentPath.transform.rotation.eulerAngles.y;

            if (randDirIndex == 0) //Top
            {
                Debug.Log("Top");
                currentDir = Direction.Top;
                currentPath = Instantiate(normalPath,
                                          currentPath.transform.GetChild(0).transform.GetChild(randDirIndex).transform.position,
                                          currentPath.transform.rotation);
            }
            else if (randDirIndex == 1) //Left
            {
                currentDir = Direction.Left;
                Debug.Log("Left");
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = rotationY - 90;
                currentPath = Instantiate(normalPath,
                                          currentPath.transform.GetChild(0).transform.GetChild(randDirIndex).transform.position,
                                          Quaternion.Euler(rotationVector));
            }
            else if (randDirIndex == 2) //Right
            {
                currentDir = Direction.Right;
                Debug.Log("Right");
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = rotationY + 90;
                currentPath = Instantiate(normalPath,
                                          currentPath.transform.GetChild(0).transform.GetChild(randDirIndex).transform.position,
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

    private void ChoosePathDirection()
    {
        //int randInt = Random.Range(0, 100);
        if (currentDir == Direction.Left)
        {
            //randDirIndex = randInt < 10 ? 0 : 2;
            randDirIndex = 2;
        }
        else if (currentDir == Direction.Right)
        {
            //randDirIndex = randInt < 10 ? 0 : 1;
            //randDirIndex = Random.Range(0, 2);
            randDirIndex = 1;
        }
        else
        {
            randDirIndex = Random.Range(0, 3);
        }
        //Debug.Log(randDirIndex);
    }
}
