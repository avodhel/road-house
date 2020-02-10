using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CarMovement()
    {
        rb.velocity = transform.forward * speed;
        CarDistance.DistanceCalculater(CarDistanceState.Start);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CarDistance.DistanceCalculater(CarDistanceState.Stop);
        UI.UIManager.GameOver();
    }
}
