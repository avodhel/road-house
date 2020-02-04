﻿using System.Collections;
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

    private void Update()
    {
        UserInput();
    }

    private void UserInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move();
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move();
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

    }

    private void Move()
    {
        rb.velocity = transform.forward * speed;
    }
}