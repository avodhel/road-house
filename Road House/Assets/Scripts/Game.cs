using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float bestDistance;
    public int collectedCoins;
    public CarModel selectedCar;

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
        }
        else
        {
            bestDistance = 0.0f;
            collectedCoins = 0;
            selectedCar = CarModel.Polo;

            SaveGameData();
        }
    }
    #endregion
}
