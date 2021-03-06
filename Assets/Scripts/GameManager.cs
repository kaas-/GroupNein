﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    private static PlayState _playState;

    public enum PlayState { StartScreen, Playing, EndScreen };


    public static int amountOfEnemies = 4;
    public static Vector3[,] EnemPatrolPoints = new Vector3[4,3];

    public GameObject mummyEne;
    public static GameObject mummyEne2;
    public GameObject scarabPrefab;
    public static GameObject scarab;
    private static GameObject[] scarabArray = new GameObject[3];
    private static GameObject[] enemies;
    private static Enemy[] enemyScripts;
    private static Enemy _enemy;
    private static GameObject _player;


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
        mummyEne2 = mummyEne;
        scarab = scarabPrefab;
        //setting up Patrol Points
        EnemPatrolPoints[0, 0] = new Vector3(41, 0.9f, -49);
        EnemPatrolPoints[0, 1] = new Vector3(0, 0.9f, -49);
        EnemPatrolPoints[0, 2] = new Vector3(-23.5f, 0.9f, -71);

        EnemPatrolPoints[1, 0] = new Vector3(-31, 0.9f, -84);
        EnemPatrolPoints[1, 1] = new Vector3(-60.5f, 0.9f, -68);
        EnemPatrolPoints[1, 2] = new Vector3(-37, 0.9f, -40);

        EnemPatrolPoints[2, 0] = new Vector3(-31, 0.9f, 22);
        EnemPatrolPoints[2, 1] = new Vector3(-54, 0.9f, -15);
        EnemPatrolPoints[2, 2] = new Vector3(-54, 0.9f, 20);

        EnemPatrolPoints[3, 0] = new Vector3(105, 0.9f, -43.5f);
        EnemPatrolPoints[3, 1] = new Vector3(50, 0.9f, -65);
        EnemPatrolPoints[3, 2] = new Vector3(80, 0.9f, -30);


        enemies = new GameObject[amountOfEnemies];
        enemyScripts = new Enemy[amountOfEnemies];
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = Instantiate(mummyEne);
            enemyScripts[i] = enemies[i].GetComponent<Enemy>();
            enemyScripts[i].setPatrolPoint(new Vector3[3] { EnemPatrolPoints[i, 0], EnemPatrolPoints[i, 1], EnemPatrolPoints[i, 02] });
        }
        scarabArray[0] = Instantiate(scarab, new Vector3(41.5f, 2f, -21.22f), Quaternion.Euler(new Vector3(90, 90, -90)));
        scarabArray[1] = Instantiate(scarab, new Vector3(116f, 2f, 6f), Quaternion.Euler(new Vector3(90, 90, -90)));
        scarabArray[2] = Instantiate(scarab, new Vector3(-80f, 2f, -51f), Quaternion.Euler(new Vector3(90, 90, -90)));

        foreach(GameObject scarab in scarabArray)
        {
            scarab.tag = "Scarab";
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playState = PlayState.StartScreen;
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(_player.transform.position);
        switch (_playState)
        {
            case PlayState.StartScreen:
                InputManager.CheckStartButton();
                break;
            case PlayState.Playing:
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemyScripts[i].stateControl(_player, !Player.IsRunning);
                }
                InputManager.CheckEButton();
                InputManager.CheckRunButton();
                Player.CheckInteractables();

                break;
            case PlayState.EndScreen:
                InputManager.CheckResetButton();
                break;
            default:
                break;
        }


    }

    public static void StartGame()
    {
        _playState = PlayState.Playing;
        UIManager.StartGame();
        print("Started");
    }

    public static void EndGame()
    {
        _playState = PlayState.EndScreen;
        UIManager.EndGame();
    }

    public static void ResetGame()
    {

        _playState = PlayState.StartScreen;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        enemies = new GameObject[amountOfEnemies];
        enemyScripts = new Enemy[amountOfEnemies];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = Instantiate(mummyEne2);
            enemyScripts[i] = enemies[i].GetComponent<Enemy>();
            enemyScripts[i].setPatrolPoint(new Vector3[3] { EnemPatrolPoints[i, 0], EnemPatrolPoints[i, 1], EnemPatrolPoints[i, 02] });
        }
        _player.transform.position = new Vector3(123.59f, 1.28f, -89.3f);
        Player.resetScarabs();
        UIManager.ResetScarabs();

        for (int i = 0; i < 3; i++)
        {
            if(scarabArray[i] != null)
            {
                Destroy(scarabArray[0]);
            }
        }
        scarabArray[0] = Instantiate(scarab, new Vector3(41.5f, 2f, -21.22f), Quaternion.Euler(new Vector3(90, 90, -90)));
        scarabArray[1] = Instantiate(scarab, new Vector3(116f, 2f, 6f), Quaternion.Euler(new Vector3(90, 90, -90)));
        scarabArray[2] = Instantiate(scarab, new Vector3(-80f, 2f, -51f), Quaternion.Euler(new Vector3(90, 90, -90)));

        UIManager.ResetGame();
    }

    public static void Interact()
    {
        Debug.Log("Interacting");
        Player.Interact();
        
    }

}
