using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliding : MonoBehaviour
{
    [Header("Trigger Collider")]
    public Collider triggerCollider;

    public bool isTriggerColliding;

    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        print("I am inside of a trigger");
        if (other.gameObject.CompareTag(GameManager.playerTag) || other.gameObject.CompareTag(GameManager.cubeTag))
        {
            isTriggerColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("I am exiting a trigger");
        if (other.gameObject.CompareTag(GameManager.playerTag) || other.gameObject.CompareTag(GameManager.cubeTag))
        {
            isTriggerColliding = false;
        }
    }
}
