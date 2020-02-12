using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDistance : MonoBehaviour
{
    public Text distanceText;
    public static float distanceValue = 0.0f;

    private static bool carMovingControl = false;

    private void Update()
    {
        if (carMovingControl)
        {
            distanceValue += Time.deltaTime;
            distanceText.text = distanceValue.ToString("F1");
        }
    }

    public static float GetCurrentDistance()
    {
        return distanceValue;
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
