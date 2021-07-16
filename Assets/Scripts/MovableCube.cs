using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCube : MonoBehaviour
{
    public float zPos2d;
    public Rigidbody rb;

    public LayerMask playerMask;
    public Collider boxCollider;

    float savedZPos;

    bool wallDetected;

    public bool startBehindWall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();

        SwitchTo2D();
    }

    private bool Behind3DWall()
    {
        if (Physics.Raycast(transform.position, Vector3.back, 100, GameManager.groundMask) || Physics.Raycast(transform.position, Vector3.back, 100, playerMask) && !startBehindWall)
        {
            return true;
        }
        else if (startBehindWall)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(GameManager.switchKey))
        {
            if (GameManager.is3D)
            {
                SwitchTo3D();
            }
            else
            {
                SwitchTo2D();
            }
        }

        if (GameManager.is3D)
        {
            When3D();
        }
        else
        {
            When2D();
        }
    }

    void SwitchTo3D()
    {
        startBehindWall = false;
        SetCubeLocation(new Vector3(transform.position.x, transform.position.y, savedZPos), false);
    }

    void SwitchTo2D()
    {
        print($"Saving z pos, current z pos is {transform.position.z}");
        savedZPos = transform.position.z;
        if (!Behind3DWall())
        {
            rb.velocity = Vector3.zero;
            SetCubeLocation(new Vector3(transform.position.x, transform.position.y, zPos2d), false);
            wallDetected = false;
            print("No wall, all good");
        }
        else
        {
            print("AHHH THERE'S A WALL HERE");
            wallDetected = true;
            return;
        }
    }

    void When2D()
    {
        if (wallDetected && !Behind3DWall())
        {
            print("There is no longer a wall detected");
            SetCubeLocation(new Vector3(transform.position.x, transform.position.y, zPos2d), false);
            wallDetected = false;
        }
        else if (!wallDetected)
        {
            SetCubeLocation(new Vector3(transform.position.x, transform.position.y, zPos2d), false);
        }
        else
        {
            SetCubeLocation(new Vector3(transform.position.x, transform.position.y, savedZPos), false);
        }
    }

    void When3D()
    {
    }

    public void SetCubeLocation(Vector3 location, bool stopVelocity)
    {
        transform.position = location;
        if (stopVelocity)
        {
            rb.velocity = Vector3.zero;
        }
        Debug.Log("Teleporting cube to " + location);
    }
}
