using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public GameObject playerCar;
    public Button previousCarButton;
    public Button nextCarButton;
    public Button upgradeButton;
    public Text carPriceText;

    private static readonly List<int> priceList = new List<int>{0, 55, 95, 115, 135, 165, 195, 220};

    private void OnEnable()
    {
        NextAndPreviouseButtonsActiveState();
        GetCarPrices();
    }

    private void GetCarPrices()
    {
        carPriceText.text = priceList[(int)Upgrade.currentCar - 1].ToString();
        UpgradeButtonActiveState();
    }

    private void UpgradeButtonActiveState()
    {
        if (Game.gameManager.collectedCoins >= priceList[(int)Upgrade.currentCar - 1])
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    private void NextAndPreviouseButtonsActiveState()
    {
        if ((int)Upgrade.currentCar == 1)
        {
            previousCarButton.gameObject.SetActive(false);
        }
        if ((int)Upgrade.currentCar == playerCar.transform.childCount)
        {
            nextCarButton.gameObject.SetActive(false);
        }
        if ((int)Upgrade.currentCar > 1 && (int)Upgrade.currentCar < playerCar.transform.childCount - 1)
        {
            previousCarButton.gameObject.SetActive(true);
            nextCarButton.gameObject.SetActive(true);
        }
    }

    public void NextCar()
    {
        if ((int)Upgrade.currentCar < playerCar.transform.childCount)
        {
            playerCar.transform.GetChild((int)Upgrade.currentCar - 1).gameObject.SetActive(false);
            Upgrade.currentCar += 1;
            playerCar.transform.GetChild((int)Upgrade.currentCar - 1).gameObject.SetActive(true);
        }
        NextAndPreviouseButtonsActiveState();
        GetCarPrices();
    }

    public void PreviousCar()
    {
        if ((int)Upgrade.currentCar < playerCar.transform.childCount + 1)
        {
            playerCar.transform.GetChild((int)Upgrade.currentCar - 1).gameObject.SetActive(false);
            Upgrade.currentCar -= 1;
            playerCar.transform.GetChild((int)Upgrade.currentCar - 1).gameObject.SetActive(true);
        }
        NextAndPreviouseButtonsActiveState();
        GetCarPrices();
    }
}
