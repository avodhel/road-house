using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button PreviousCarButton;
    public Button NextCarButton;
    public GameObject playerCar;

    public static CarModel currentCar;

    private void Start()
    {
        currentCar = Upgrade.currentCar;
        ButtonsActiveState();
    }

    private void ButtonsActiveState()
    {
        if ((int)currentCar == 1)
        {
            PreviousCarButton.gameObject.SetActive(false);
        }
        if ((int)currentCar == playerCar.transform.childCount)
        {
            NextCarButton.gameObject.SetActive(false);
        }
        if ((int)currentCar > 1 && (int)currentCar < playerCar.transform.childCount - 1)
        {
            PreviousCarButton.gameObject.SetActive(true);
            NextCarButton.gameObject.SetActive(true);
        }
    }

    public void NextCar()
    {
        if ((int)currentCar < playerCar.transform.childCount)
        {
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(false);
            currentCar += 1;
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(true);
        }
        ButtonsActiveState();
    }

    public void PreviousCar()
    {
        if ((int)currentCar < playerCar.transform.childCount + 1)
        {
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(false);
            currentCar -= 1;
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(true);
        }
        ButtonsActiveState();
    }
}
