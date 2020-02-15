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
    public Button selectButton;
    public Image lockImage;
    public Text carPriceText;

    public static CarModel currentCar;

    private delegate void PrepareUpgradeScene();
    private PrepareUpgradeScene prepareUpgradeScene;

    private List<bool> lockList = new List<bool>();
    private static readonly List<int> priceList = new List<int> { 0, 2, 4, 6, 8, 10, 12, 14 };

    private void Awake()
    {
        prepareUpgradeScene += NextAndPreviousButtonsActiveState;
        prepareUpgradeScene += GetCarPrices;
        prepareUpgradeScene += CarsLockState;
        prepareUpgradeScene += SelectButtonInteractableState;
    }

    private void OnEnable()
    {
        currentCar = SaveLoadSystem.LoadGameData().selectedCar;
        lockList = SaveLoadSystem.LoadGameData().lockList;
        prepareUpgradeScene();
    }

    private void CarsLockState()
    {
        if (lockList[(int)currentCar - 1])
        {
            lockImage.enabled = true;
            upgradeButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }
        else //unlocked
        {
            lockImage.enabled = false;
            upgradeButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
    }

    private void GetCarPrices()
    {
        carPriceText.text = priceList[(int)currentCar - 1].ToString();
        UpgradeButtonInteractableState();
    }

    private void UpgradeButtonInteractableState()
    {
        if (Game.gameManager.collectedCoins >= priceList[(int)currentCar - 1])
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    private void SelectButtonInteractableState()
    {
        bool carLockUnlockControl = lockList[(int)currentCar - 1];
        CarModel selectedCar = SaveLoadSystem.LoadGameData().selectedCar;
        if (!carLockUnlockControl && currentCar == selectedCar)
        {
            selectButton.interactable = false;
            selectButton.transform.GetChild(0).gameObject.SetActive(false);
            selectButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (!carLockUnlockControl && currentCar != selectedCar)
        {
            selectButton.interactable = true;
            selectButton.transform.GetChild(0).gameObject.SetActive(true);
            selectButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void NextAndPreviousButtonsActiveState()
    {
        if ((int)currentCar == 1)
        {
            previousCarButton.gameObject.SetActive(false);
        }
        if ((int)currentCar == playerCar.transform.childCount)
        {
            nextCarButton.gameObject.SetActive(false);
        }
        if ((int)currentCar > 1 && (int)currentCar < playerCar.transform.childCount)
        {
            previousCarButton.gameObject.SetActive(true);
            nextCarButton.gameObject.SetActive(true);
        }
    }

    public void CarUnlocked()
    {
        Game.gameManager.CarUnlockSystem((int)currentCar - 1, priceList[(int)currentCar - 1]);
        lockList = SaveLoadSystem.LoadGameData().lockList;
        CarSelected();
        CarsLockState();
    }

    public void CarSelected()
    {
        Game.gameManager.CarSelected(currentCar);
        selectButton.interactable = false;
        selectButton.transform.GetChild(0).gameObject.SetActive(false);
        selectButton.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void NextCar()
    {
        if ((int)currentCar < playerCar.transform.childCount)
        {
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(false);
            currentCar += 1;
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(true);
        }
        prepareUpgradeScene();
    }

    public void PreviousCar()
    {
        if ((int)currentCar < playerCar.transform.childCount + 1)
        {
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(false);
            currentCar -= 1;
            playerCar.transform.GetChild((int)currentCar - 1).gameObject.SetActive(true);
        }
        prepareUpgradeScene();
    }
}
