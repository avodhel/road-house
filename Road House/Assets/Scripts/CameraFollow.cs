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

    private void Update()
    {
        transform.position = car.transform.position + offset;
        DrawRay();
    }

    private void CalculateOffset()
    {
        offset = transform.position - car.transform.position;
    }

    #region Draw Raycast

    private void DrawRay()
    {
        RaycastHit[] hits = hits = Physics.RaycastAll(transform.position,
                                                      transform.forward, 
                                                      100.0F);
        foreach (RaycastHit h in hits)
        {
            if (h.transform.tag == "treeTag")
            {
                Debug.Log("Hit!");
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
