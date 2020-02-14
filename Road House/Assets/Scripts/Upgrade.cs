using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject playerCar;

    public static CarModel currentCar;

    private void Start()
    {
        currentCar = SaveLoadSystem.LoadGameData().selectedCar;
        PrepareCurrentCar();
    }

    private void PrepareCurrentCar()
    {
        for (int i = 0; i < playerCar.transform.childCount; i++)
        {
            if (i == (int)currentCar - 1)
            {
                playerCar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                playerCar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
