using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Car car;

    private void Start()
    {
        car = gameObject.GetComponent<Car>();
    }

    private void Update()
    {
        UserInputDetect();
    }

    private void UserInputDetect()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            car.CarMovement();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            car.CarMovement();
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
