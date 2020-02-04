using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject car;

    private Vector3 offset;

    private void Start()
    {
        CalculateOffset();
    }

    private void CalculateOffset()
    {
        offset = transform.position - car.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = car.transform.position + offset;
    }
}
