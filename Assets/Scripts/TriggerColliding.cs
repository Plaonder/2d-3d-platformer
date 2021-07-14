using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliding : MonoBehaviour
{
    [Header("Trigger Collider")]
    public Collider triggerCollider;

    [Header("Player Tag")]
    public string playerTag = "Player";

    public bool isTriggerColliding;

    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        print("I am inside of a trigger");
        if (other.gameObject.CompareTag(playerTag))
        {
            isTriggerColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("I am exiting a trigger");
        if (other.gameObject.CompareTag(playerTag))
        {
            isTriggerColliding = false;
        }
    }
}
