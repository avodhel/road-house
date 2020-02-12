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
        if (currentMoveState == CarMoveState.StartEngine)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            currentMoveState = CarMoveState.TurnLeft;
        }
        else if (currentMoveState == CarMoveState.GoStraight)
        {
            FindObjectOfType<SFX>().PlaySoundEffect("turn");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            currentMoveState = CarMoveState.TurnLeft;
        }
        else if (currentMoveState == CarMoveState.TurnLeft)
        {
            FindObjectOfType<SFX>().PlaySoundEffect("turn");
            transform.rotation = Quaternion.Euler(0, -90, 0);
            currentMoveState = CarMoveState.GoStraight;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        carCrashedControl = true;
        FindObjectOfType<SFX>().PlaySoundEffect("crash");
        UI.UIManager.GameOver();
    }
}
