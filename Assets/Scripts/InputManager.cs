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

    public static void CheckEButton()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Interact();
        }
    }
    

    public static void CheckRunButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Player.IsRunning = true;
        }
    }

    public static void CheckResetButton()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.ResetGame();
        }
    }
}
