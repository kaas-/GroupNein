using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    private static bool playing = false;

    private Enemy _enemy;
    private GameObject _player;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(_player.transform.position);
        if (playing)
        {
            _enemy.stateControl(_player, !Player.IsRunning);
            InputManager.CheckEButton();
            InputManager.CheckRunButton();
        }
        else
        {
            InputManager.CheckStartButton();
        }

    }

    public static void StartGame()
    {
        playing = true;
        print("Started");
    }

    public static void EndGame()
    {
        playing = false;
    }

    public static void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void Interact()
    {
        Debug.Log("Interacting");
        Player.Interact();
        //UIManager.Test();
    }


    public static bool GetPlaying()
    {
        return playing;
    }
}
