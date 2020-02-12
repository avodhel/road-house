using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float bestDistance;

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
        }
        else
        {
            bestDistance = 0.0f;
        }
    }
    #endregion
}
