using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : Car
{
    public bool carCrashedControl = false;

    public override void CarMovement()
    {
        rb.velocity = transform.forward * speed;
        CarDistance.DistanceCalculater(CarDistanceState.Start);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        carCrashedControl = true;
        CarDistance.DistanceCalculater(CarDistanceState.Stop);
        UI.UIManager.GameOver();
    }
}
