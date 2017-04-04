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
        Debug.Log("Check E Button");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            GameManager.Interact();
        }
    }
}
