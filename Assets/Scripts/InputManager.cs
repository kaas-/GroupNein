using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static InputManager instance = null;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

    }

    public static void CheckStartButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.StartGame();
        }
    }

    public static void CheckWButton()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameManager.PlayerForward();
        }
    }

    public static void CheckAButton()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.PlayerLeft();
        }
    }

    public static void CheckSButton()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameManager.PlayerBack();
        }
    }

    public static void CheckDButton()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.PlayerRight();
        }
    }
}
