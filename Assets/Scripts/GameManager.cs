using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float _timeLimit;

    private static float timeLimit;

    private static GameManager instance = null;
    private static bool playing = false;

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

        timeLimit = _timeLimit;
    }

    // Update is called once per frame
    void Update()
    {

        if (playing)
        {
            InputManager.CheckShootButton();
        }
        else
        {
            InputManager.CheckStartButton();
        }

    }

    public static void StartGame()
    {
        playing = true;

        if (timeLimit < 100)
        {
            print("HI!");
        }
    }

    public static void EndGame()
    {
        playing = false;
    }

    public static void Shoot()
    {
        print("PEW!");
    }

    public static bool GetPlaying()
    {
        return playing;
    }
}
