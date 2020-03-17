using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectUI : MonoBehaviour
{
    [Header("Camera")]
    public Camera mainCamera;
    public Camera carSelectCamera;

    [Header("Car")]
    public GameObject playerCar;
    //public CarData[] carDatas;

    [Header("Lock Unlock Scene")]
    public Button previousCarButton;
    public Button nextCarButton;
    public Button carUnlockButton;
    public Button selectButton;
    public Image lockImage;
    public Text carPriceText;

    private static CarData currentCar;
    private static int carNo;

    private delegate void PrepareCarSelectScene();
    private PrepareCarSelectScene prepareCarSelectScene;

    private void Awake()
    {
        prepareCarSelectScene += NextAndPreviousButtonsActiveState;
        prepareCarSelectScene += GetCarPrices;
        prepareCarSelectScene += CarsLockState;
        prepareCarSelectScene += SelectButtonInteractableState;
    }

    private void OnEnable()
    {
        mainCamera.gameObject.SetActive(false);
        carSelectCamera.gameObject.SetActive(true);

        currentCar = Game.gameManager.GetSelectedCarData();
        carNo = currentCar.Id;
        prepareCarSelectScene();
    }

    private void OnDisable()
    {
        mainCamera.gameObject.SetActive(true);
        carSelectCamera.gameObject.SetActive(false);
    }

    private void CarsLockState()
    {
        if (Game.gameManager.carDatas[carNo].unlocked)
        {
            lockImage.enabled = false;
            carUnlockButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
        else
        {
            lockImage.enabled = true;
            carUnlockButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }
    }

    private void GetCarPrices()
    {
        carPriceText.text = Game.gameManager.carDatas[carNo].price.ToString();
        CarUnlockButtonInteractableState();
    }

    private void CarUnlockButtonInteractableState()
    {
        if (Game.gameManager.CheckCoinAccount(carNo))
        {
            carUnlockButton.interactable = true;
        }
        else
        {
            carUnlockButton.interactable = false;
        }
    }

    private void SelectButtonInteractableState()
    {
        bool carLockUnlockControl = Game.gameManager.carDatas[carNo].unlocked;
        CarModel selectedCarModel = Game.gameManager.selectedCar;

        if (carLockUnlockControl && Game.gameManager.carDatas[carNo].carModel == selectedCarModel)
        {
            selectButton.interactable = false;
            selectButton.transform.GetChild(0).gameObject.SetActive(false);
            selectButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (carLockUnlockControl && Game.gameManager.carDatas[carNo].carModel != selectedCarModel)
        {
            selectButton.interactable = true;
            selectButton.transform.GetChild(0).gameObject.SetActive(true);
            selectButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void NextAndPreviousButtonsActiveState()
    {
        if (carNo == 0)
        {
            previousCarButton.gameObject.SetActive(false);
        }
        if (carNo == playerCar.transform.childCount - 1)
        {
            nextCarButton.gameObject.SetActive(false);
        }
        if (carNo > 0 && carNo < playerCar.transform.childCount - 1)
        {
            previousCarButton.gameObject.SetActive(true);
            nextCarButton.gameObject.SetActive(true);
        }
    }

    public void CarUnlocked()
    {
        Game.gameManager.CarUnlockSystem(carNo);
        CarSelected();
        CarsLockState();
    }

    public void CarSelected()
    {
        Game.gameManager.CarSelected(carNo);
        selectButton.interactable = false;
        selectButton.transform.GetChild(0).gameObject.SetActive(false);
        selectButton.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void NextCar()
    {
        if (carNo < playerCar.transform.childCount)
        {
            playerCar.transform.GetChild(carNo).gameObject.SetActive(false);
            carNo += 1;
            playerCar.transform.GetChild(carNo).gameObject.SetActive(true);
        }
        prepareCarSelectScene();
    }

    public void PreviousCar()
    {
        if (carNo < playerCar.transform.childCount + 1)
        {
            playerCar.transform.GetChild(carNo).gameObject.SetActive(false);
            carNo -= 1;
            playerCar.transform.GetChild(carNo).gameObject.SetActive(true);
        }
        prepareCarSelectScene();
    }
}
