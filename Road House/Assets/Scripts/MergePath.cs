using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePath : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "carTag")
        {
            for (int i = 0; i < 5; i++)
            {
                Spawn.SpawnManager.SpawnPath();
            }
        }
    }
}
