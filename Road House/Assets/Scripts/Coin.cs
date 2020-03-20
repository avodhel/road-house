using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerCarTag")
        {
            gameObject.SetActive(false);
            Game.gameManager.CoinCollected();
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
