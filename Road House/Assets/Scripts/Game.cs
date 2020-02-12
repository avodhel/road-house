using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float bestDistance;
    public int collectedCoins;

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
    }

    private void Start()
    {
        //SaveLoadSystem.DeleteDatas();
        LoadGameData();
    }

    public void CoinCollected()
    {
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
        }
        else
        {
            bestDistance = 0.0f;
            collectedCoins = 0;
        }
    }
    #endregion
}
