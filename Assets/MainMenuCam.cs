using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraSpeed;
    public float rotateSpeed;

    public float camHeight3d;

    public float cameraOffset;
    public float cameraOffset3d;

    public Camera cam;

    Vector3 savedCameraPosition;
    Quaternion savedCameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.is3D = true;
        cam = GetComponent<Camera>();

        savedCameraPosition = transform.position;
        savedCameraRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.is3D)
        {
            cam.orthographic = false;

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, savedCameraRotation, cameraSpeed);
            transform.position = Vector3.Lerp(transform.position, savedCameraPosition, cameraSpeed);
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
