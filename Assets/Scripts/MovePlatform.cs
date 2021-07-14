using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("2D Position")]
    public float zPos2d;
    float savedZPos;

    public TriggerColliding triggerColliding;

    //Vector3 rayDirection = Vector3.forward * -1;
    //float maxDistance = 100;

    void Start()
    {
        savedZPos = transform.position.z;
    }

    // Update is called once per frame
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

        if(!GameManager.is3D)
        {
            When2D();
        }
    }

    void SwitchTo3D()
    {
        MoveToSaved3DPos();
    }

    void SwitchTo2D()
    {
        if (!triggerColliding.isTriggerColliding)
        {
            MoveTo2DPos();
        }
    }

    void When2D()
    {
        if(!triggerColliding.isTriggerColliding)
        {
            MoveTo2DPos();
        }
    }

    void MoveTo2DPos()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos2d);   
    }

    void MoveToSaved3DPos()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, savedZPos);
    }

}