using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraSpeed;
    public float rotateSpeed;

    public float camHeight3d;

    public float cameraOffset;

    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();    
    }

    void FixedUpdate()
    {
        if(GameManager.is3D)
        {
            cam.orthographic = false;

            Vector3 targetPosition = playerTransform.position + (Vector3.right * cameraOffset) + (Vector3.up * camHeight3d);

            var rotation = Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        }
        else
        {
            cam.orthographic = true;

            Vector3 targetPosition = playerTransform.position + Vector3.forward * cameraOffset;

            var rotation = Quaternion.Euler(0, 0, 0); // this resets the y rotation

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }
    }
}
