using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject currentPath;
    public GameObject mergePath;
    public GameObject normalPath;
    public GameObject[] paths;

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        if (currentPath.tag == "mergePathTag")
        {
            int randPosIndex = Random.Range(0, 3);
            float rotationY = currentPath.transform.rotation.eulerAngles.y;
            //Quaternion newRotation;

            if (randPosIndex == 0) //Top
            {
                Debug.Log("Top");
                currentPath = Instantiate(normalPath, 
                                          currentPath.transform.GetChild(0).transform.GetChild(randPosIndex).transform.position,
                                          currentPath.transform.rotation);
            }
            else if (randPosIndex == 1) //Left
            {
                Debug.Log("Left");
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = rotationY - 90;
                currentPath = Instantiate(normalPath, 
                                          currentPath.transform.GetChild(0).transform.GetChild(randPosIndex).transform.position,
                                          Quaternion.Euler(rotationVector));
            }
            else if (randPosIndex == 2) //Right
            {
                Debug.Log("Right");
                var rotationVector = transform.rotation.eulerAngles;
                rotationVector.y = rotationY + 90;
                currentPath = Instantiate(normalPath, 
                                          currentPath.transform.GetChild(0).transform.GetChild(randPosIndex).transform.position,
                                          Quaternion.Euler(rotationVector));
            }
        }
        else if (currentPath.tag == "normalPathTag")
        {
            Debug.Log("Top");
            currentPath = Instantiate(paths[Random.Range(0, paths.Length)], 
                                      currentPath.transform.GetChild(1).transform.position, 
                                      currentPath.transform.rotation);
        }

    }
}
