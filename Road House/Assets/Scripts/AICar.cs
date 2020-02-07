using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    public Color[] colors;

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        ChooseCarColor();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void ChooseCarColor()
    {
        gameObject.transform.GetComponent<MeshRenderer>().materials[0].color = colors[Random.Range(0, colors.Length)];
    }

    public Vector3 StartedRotation(float normalPathRot)
    {
         Vector3 newRot = new Vector3(gameObject.transform.rotation.eulerAngles.x,
                                      gameObject.transform.rotation.eulerAngles.y + normalPathRot,
                                      gameObject.transform.rotation.eulerAngles.z);
        return newRot;
    }

    private void Movement()
    {
        rb.velocity = transform.right * 5f;
    }

    private IEnumerator RotateCar(PathDirection mergePathDir, float waitTimeForRotate)
    {
        Vector3 objectRotation = Vector3.zero;
        yield return new WaitForSeconds(waitTimeForRotate);

        if (mergePathDir == PathDirection.Top)
        {
             objectRotation = new Vector3(gameObject.transform.rotation.eulerAngles.x,
                                          gameObject.transform.rotation.eulerAngles.y,
                                          gameObject.transform.rotation.eulerAngles.z);
        }
        if (mergePathDir == PathDirection.Left)
        {
            objectRotation = new Vector3(gameObject.transform.rotation.eulerAngles.x,
                                         gameObject.transform.rotation.eulerAngles.y + 90,
                                         gameObject.transform.rotation.eulerAngles.z);
        }
        if (mergePathDir == PathDirection.Right)
        {
            objectRotation = new Vector3(gameObject.transform.rotation.eulerAngles.x,
                                         gameObject.transform.rotation.eulerAngles.y - 90,
                                         gameObject.transform.rotation.eulerAngles.z);
        }

        gameObject.transform.rotation = Quaternion.Euler(objectRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "mergePathTag")
        {
            PathDirection mergePathDir = other.GetComponent<MergePath>().mergePathDir;
            if (mergePathDir == PathDirection.Left)
            {
                StartCoroutine(RotateCar(mergePathDir, 0.5f));
            }
            if (mergePathDir == PathDirection.Right)
            {
                StartCoroutine(RotateCar(mergePathDir, 1f));
            }
        }
    }
}
