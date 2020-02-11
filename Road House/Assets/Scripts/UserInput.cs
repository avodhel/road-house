using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private PlayerCar playerCar;

    private void Start()
    {
        playerCar = gameObject.GetComponent<PlayerCar>();
    }

    private void Update()
    {
        if (!playerCar.carCrashedControl)
        {
            UserInputDetect();
        }
    }

    private void UserInputDetect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerCar.CarMovement();
        }
    }
}
