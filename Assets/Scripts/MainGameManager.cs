using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public KeyCode switchKey;
    public LayerMask groundMask;

    [SerializeField]
    public string playerTag;
    [SerializeField]
    public string cubeTag;
    // Update is called once per frame
    void Update()
    {
        GameManager.playerTag = playerTag;
        GameManager.cubeTag = cubeTag;

        GameManager.groundMask = groundMask;
        GameManager.switchKey = switchKey;
        if(Input.GetKeyDown(GameManager.switchKey))
        {
            GameManager.is3D = !GameManager.is3D;
        }
    }
}
