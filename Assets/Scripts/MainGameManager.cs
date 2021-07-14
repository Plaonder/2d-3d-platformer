using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public KeyCode switchKey;
    // Update is called once per frame
    void Update()
    {
        GameManager.switchKey = switchKey;
        if(Input.GetKeyDown(GameManager.switchKey))
        {
            GameManager.is3D = !GameManager.is3D;
        }
    }
}
