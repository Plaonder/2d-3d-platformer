using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public KeyCode switchKey;
    public LayerMask groundMask;

    float timer;
    float holdDur = 0.3f;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            if (Time.time - timer > holdDur)
            {
                //by making it positive inf, we won't subsequently run this code by accident,
                //since X - +inf = -inf, which is always less than holdDur
                timer = float.PositiveInfinity;

                SceneManager.LoadSceneAsync(0);
                print("I will be quitting");
            }
        }
        else
        {
            timer = float.PositiveInfinity;
        }



        if (Input.GetKeyDown(GameManager.switchKey))
        {
            GameManager.is3D = !GameManager.is3D;
        }
    }
}
