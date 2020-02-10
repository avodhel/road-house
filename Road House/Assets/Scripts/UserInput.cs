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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerCar.CarMovement();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerCar.CarMovement();
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
