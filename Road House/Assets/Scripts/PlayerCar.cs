using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : Car
{
    [HideInInspector]
    public bool carCrashedControl = false;
    private CarMoveState currentMoveState;

    public override void CarMovement()
    {
        RotateCar();
        rb.velocity = gameObject.transform.forward * speed;
        CarDistance.DistanceCalculater(CarDistanceState.Start);
    }

    private void RotateCar()
    {
        if (currentMoveState == CarMoveState.TurnLeft)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            currentMoveState = CarMoveState.GoStraight;
        }
        else if (currentMoveState == CarMoveState.GoStraight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            currentMoveState = CarMoveState.TurnLeft;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        carCrashedControl = true;
        CarDistance.DistanceCalculater(CarDistanceState.Stop);
        UI.UIManager.GameOver();
    }
}
