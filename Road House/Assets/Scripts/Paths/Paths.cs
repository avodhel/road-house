using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "playerCarTag")
        {
            Spawn.SpawnManager.MultiplePathsSpawner(1);
            StartCoroutine(DeactivatePath());
        }
    }

    protected IEnumerator DeactivatePath()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
