using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    private static bool playing = false;
    private static bool _playerHasKey = false;
    public static bool PlayerHasKey
    {
        get { return _playerHasKey; }
        set { _playerHasKey = value; }
    }

    private Enemy _enemy;
    private GameObject _player;
    public GameObject Player
    {
        get { return _player; }
    }

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
            _enemy.stateControl(_player);
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

    }

    private void _interact()
    {

    }

    public static bool GetPlaying()
    {
        return playing;
    }
}
