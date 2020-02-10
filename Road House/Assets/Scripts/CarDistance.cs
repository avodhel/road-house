using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDistance : MonoBehaviour
{
    public static float distanceValue = 0.0f;

    private static Text distanceText;
    private static bool carMovingControl = false;

    private void Start()
    {
        distanceText = UI.UIManager.distanceText;
        ShowDistance();
    }

    private void Update()
    {
        if (carMovingControl)
        {
            distanceValue += Time.deltaTime;
            ShowDistance();
        }
    }

    private void ShowDistance()
    {
        distanceText.text = distanceValue.ToString("F1");
    }

    public static void DistanceCalculater(CarDistanceState state)
    {
        if (state == CarDistanceState.Reset)
        {
            carMovingControl = false;
            distanceValue = 0.0f;
        }
        else if (state == CarDistanceState.Start)
        {
            carMovingControl = true;
        }
        else if (state == CarDistanceState.Stop)
        {
            carMovingControl = false;
        }
    }
}
