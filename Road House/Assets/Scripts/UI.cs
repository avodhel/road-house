using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text distanceText;
    public Text bestDistanceText;
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

    private void Update()
    {
        distanceText.text = CarDistance.GetCurrentDistance().ToString("F1");
    }

    public void GameOver()
    {
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
