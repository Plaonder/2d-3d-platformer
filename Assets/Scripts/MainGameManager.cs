using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] bool is3D;

    // Update is called once per frame
    void Update()
    {
        GameManager.is3D = is3D;
    }
}
