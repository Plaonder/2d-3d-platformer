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
    public float cameraOffset3d;

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

            float zAdjust = playerTransform.position.z + cameraOffset;

            Vector3 targetPosition = playerTransform.position + (Vector3.up * camHeight3d) + (Vector3.right * cameraOffset3d);

            //find the vector pointing from our position to the target
            Vector3 _direction = (playerTransform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, cameraSpeed);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }
        else
        {
            cam.orthographic = true;

            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraOffset);

            var rotation = Quaternion.Euler(0, 0, 0); // this resets the y rotation

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }
    }
}
