using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //public Button button;
    public bool openDoor;
    public Collider coolCollider;
    public float doorSpeed;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        coolCollider = GetComponent<Collider>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(openDoor)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, -coolCollider.bounds.size.y - 0.2f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, doorSpeed);
        }
        else
        {
            Vector3 targetPosition = startPosition;
            transform.position = Vector3.Lerp(transform.position, startPosition, doorSpeed);
        }
    }
}
