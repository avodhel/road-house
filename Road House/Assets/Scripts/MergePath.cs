using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePath : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "carTag")
        {
           Spawn.SpawnManager.MultiplePathsSpawner(5);
        }
    }
}
