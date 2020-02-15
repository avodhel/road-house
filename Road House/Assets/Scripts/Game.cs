using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject playerCar;
    public float bestDistance;
    public int collectedCoins;
    public CarModel selectedCar;
    public List<bool> lockList = new List<bool>();

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

    public void CoinCollected()
    {
        FindObjectOfType<SFX>().PlaySoundEffect("coin");
        collectedCoins += 1;
        UI.UIManager.coinText.text = collectedCoins.ToString();
    }

    public void CheckBestDistance(float newDistance)
    {
        if (newDistance > bestDistance)
        {
            bestDistance = newDistance;
        }

        SaveGameData();
    }

    public void CarSelected(CarModel _selectedCar)
    {
        selectedCar = _selectedCar;
        SaveGameData();
    }

    public void CarUnlockSystem(int carValue, int carPrice)
    {
        lockList[carValue] = false;
        collectedCoins -= carPrice;
        UI.UIManager.coinText.text = collectedCoins.ToString();
        SaveGameData();
    }

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
            lockList = gameData.lockList;
        }
        else
        {
            bestDistance = 0.0f;
            collectedCoins = 0;
            selectedCar = CarModel.Polo;
            lockList = new List<bool> { false, true, true, true, true, true, true, true };

            SaveGameData();
        }
    }
    #endregion
}
