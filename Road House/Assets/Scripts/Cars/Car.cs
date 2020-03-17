using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car : MonoBehaviour
{
    [SerializeField]
    protected float speed = 5f;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public abstract void CarMovement();

    protected void StopCar()
    {
        speed = 0f;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        StopCar();
    }
}
