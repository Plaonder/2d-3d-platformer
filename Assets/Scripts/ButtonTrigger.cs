using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Door door;

    public Material redMaterial;
    public Material greenMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.cubeTag))
        {
            MeshRenderer cubeRenderer = other.gameObject.GetComponent<MeshRenderer>();
            cubeRenderer.material = greenMaterial;
            print("cool button press :D");
            door.openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.cubeTag))
        {
            MeshRenderer cubeRenderer = other.gameObject.GetComponent<MeshRenderer>();
            cubeRenderer.material = redMaterial;
            print("uncool button unpress D:");
            door.openDoor = false;
        }
    }
}
