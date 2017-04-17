using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    private static bool playing = false;
    public static int amountOfEnemies = 4;
    public static Vector3[,] EnemPatrolPoints = new Vector3[4,3];

    public GameObject mummyEne;
    public static GameObject mummyEne2;
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
        //setting up Patrol Points
        EnemPatrolPoints[0, 0] = new Vector3(123, 0.9f, -85);
        EnemPatrolPoints[0, 1] = new Vector3(100, 0.9f, -50);
        EnemPatrolPoints[0, 2] = new Vector3(100, 0.9f, -50);

        EnemPatrolPoints[1, 0] = new Vector3(123, 0.9f, -85);
        EnemPatrolPoints[1, 1] = new Vector3(100, 0.9f, -50);
        EnemPatrolPoints[1, 2] = new Vector3(100, 0.9f, -50);

        EnemPatrolPoints[2, 0] = new Vector3(123, 0.9f, -85);
        EnemPatrolPoints[2, 1] = new Vector3(100, 0.9f, -50);
        EnemPatrolPoints[2, 2] = new Vector3(100, 0.9f, -50);

        EnemPatrolPoints[3, 0] = new Vector3(123, 0.9f, -85);
        EnemPatrolPoints[3, 1] = new Vector3(100, 0.9f, -50);
        EnemPatrolPoints[3, 2] = new Vector3(100, 0.9f, -50);


        enemies = new GameObject[amountOfEnemies];
        enemyScripts = new Enemy[amountOfEnemies];
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = Instantiate(mummyEne);
            enemyScripts[i] = enemies[i].GetComponent<Enemy>();
            enemyScripts[i].setPatrolPoint(new Vector3[3] { EnemPatrolPoints[i, 0], EnemPatrolPoints[i, 1], EnemPatrolPoints[i, 02] });
        }
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(_player.transform.position);
        if (playing)
        {
            //_enemy.stateControl(_player, !Player.IsRunning);
            for(int i = 0; i < enemies.Length; i++)
            {
                enemyScripts[i].stateControl(_player, !Player.IsRunning);
            }
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
        playing = false;
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
        Player.resetHasKey();

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

    private void tester()
    {

    }
}
