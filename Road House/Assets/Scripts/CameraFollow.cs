using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject car;

    private Vector3 offset;
    private float distance;

    private void Start()
    {
        CalculateOffsetAndDistance();
    }

    private void Update()
    {
        transform.position = car.transform.position + offset;
        DrawRay();
    }

    private void CalculateOffsetAndDistance()
    {
        offset = transform.position - car.transform.position;
        distance = Vector3.Distance(transform.position, car.transform.position);
    }

    #region Draw Raycast

    private void DrawRay()
    {
        RaycastHit[] hits = hits = Physics.RaycastAll(transform.position,
                                                      transform.forward,
                                                      distance);
        foreach (RaycastHit h in hits)
        {
            if (h.transform.tag == "treeTag")
            {
                MeshRenderer meshRend = h.transform.GetComponentInChildren<MeshRenderer>();
                // make transparent
                meshRend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = meshRend.material.color;
                tempColor.a = 0.15F;
                meshRend.material.color = tempColor;
            }
        }
    }

    #endregion
}
