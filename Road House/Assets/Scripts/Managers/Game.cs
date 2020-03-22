using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerCar;

    [Header("Car Data")]
    public CarData[] carDatas;
    public CarModel selectedCar;
    public List<bool> lockList = new List<bool>();

    [Header("Game Data")]
    public float bestDistance;
    public int collectedCoins;

    [HideInInspector]
    public bool gameOverControl = false;

    private CarData selectedCarData;

    public static Game gameManager { get; private set; }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadGameData();
        //SaveLoadSystem.DeleteDatas();
    }

    private void Start()
    {
        PrepareSelectedCar();
    }

    #region Car

    public void CarSelected(int carId)
    {
        selectedCar = carDatas[carId].carModel;
        SaveGameData();
    }

    public CarData GetSelectedCarData()
    {
        foreach (var carData in carDatas)
        {
            if (carData.carModel == selectedCar)
            {
                selectedCarData = carData;
            }
        }
        return selectedCarData;
    }

    public void PrepareSelectedCar()
    {
        for (int i = 0; i < playerCar.transform.childCount; i++)
        {
            if (i == (int)selectedCar - 1)
            {
                playerCar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                playerCar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void CarUnlockSystem(int carId)
    {
        lockList[carId] = true;
        carDatas[carId].unlocked = true;
        collectedCoins -= carDatas[carId].price;
        UI.UIManager.coinText.text = collectedCoins.ToString();
        SaveGameData();
    }

    #endregion

    #region Coin

    public void CoinCollected()
    {
        FindObjectOfType<SFX>().PlaySoundEffect("coin");
        collectedCoins += 1;
        UI.UIManager.coinText.text = collectedCoins.ToString();
    }

    public bool CheckCoinAccount(int carId)
    {
        if (collectedCoins >= carDatas[carId].price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Distance

    public void CheckBestDistance(float newDistance)
    {
        if (newDistance > bestDistance)
        {
            bestDistance = newDistance;
        }

        SaveGameData();
    }

    #endregion

    #region Save and Load System
    public void SaveGameData()
    {
        SaveLoadSystem.SaveGameData(this);
    }

    public void LoadGameData()
    {
        GameData gameData = SaveLoadSystem.LoadGameData();

        if (gameData != null)
        {
            bestDistance = gameData.bestDistance;
            collectedCoins = gameData.coin;
            selectedCar = gameData.selectedCar;

            //Cars Lock/Unlock Data
            lockList = gameData.lockList;
            for (int i = 0; i < lockList.Count; i++)
            {
                carDatas[i].unlocked = lockList[i];
            }
        }
        else
        {
            bestDistance = 0.0f;
            collectedCoins = 0;
            selectedCar = CarModel.Polo;

            //Cars Lock/Unlock Data
            for (int i = 0; i < carDatas.Length; i++)
            {
                lockList.Add(carDatas[i].unlocked);
            }

            SaveGameData();
        }
    }
    #endregion
}
