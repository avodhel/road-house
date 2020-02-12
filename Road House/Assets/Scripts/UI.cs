using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text distanceText;
    public Text bestDistanceText;
    public Text coinText;
    public GameObject startPanel;
    public GameObject gameOverPanel;

    public static UI UIManager { get; private set; }

    private void Awake()
    {
        if (UIManager == null)
        {
            UIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        distanceText.text = CarDistance.GetCurrentDistance().ToString("F1");
        coinText.text = Game.gameManager.collectedCoins.ToString();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
    }

    public void GameOver()
    {
        CarDistance.DistanceCalculater(CarDistanceState.Stop);
        Game.gameManager.CheckBestDistance(CarDistance.GetCurrentDistance());
        bestDistanceText.text = "Best: " + SaveLoadSystem.LoadGameData().bestDistance.ToString("F1");
        gameOverPanel.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameOverPanel.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CarDistance.DistanceCalculater(CarDistanceState.Reset);
    }
}
