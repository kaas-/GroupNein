using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float eneWalkSpeed = 5;
    public float eneRunSpeed = 9;
    public float AttRange = 5;
    public float searchRadius = 5;
    public float searchRunRadius = 10;
    public float chaseRadius = 20;
    public float turnSpeed = 5;
    public float threshTime = 5;
    public Vector3[] patrolPoints = new[] {new Vector3(123, 0.9f, -85), new Vector3(100, 0.9f, -50), new Vector3(50, 0.9f, -65) };

    private GameObject _player;
    private Transform _lookAtTransform;
    public string state = "Patrol";
    private NavMeshAgent _navMeshAgent;
    private Vector3 curTar;
    private Vector3 lastPos;
    private float startTime;
    private Animator _anim;

    void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _anim = this.GetComponent<Animator>();
        curTar = patrolPoints[0];
        state = "Patrol";
    }

    public void stateControl(GameObject player, bool isWalking)
    {
        _player = player;
        Turn();
        Move(curTar);
        if (state == "Patrol")
        {
            Patrol();
            if (isWalking == true)
            {
                checkForPlayer();
            }
            else
            {
                checkForPlayerRun();
            }
        }
        else if(state == "Chase")
        {
            Chase();
        }
        else if(state == "Search")
        {
            if(isWalking == true)
            {
                checkForPlayer();
            }
            else
            {
                checkForPlayerRun();
            }
            
            Search();
        }
        else if (state == "Attack")
        {
            Attack();
        }
    }

    private void Patrol()
    {
        _navMeshAgent.speed = eneWalkSpeed;
        _anim.PlayInFixedTime("Walk_Weaponless");
        if (transform.position.x == patrolPoints[0].x && transform.position.z == patrolPoints[0].z)
        {
            curTar = patrolPoints[1];
        }
        else if (transform.position.x == patrolPoints[1].x && transform.position.z == patrolPoints[1].z)
        {
            curTar = patrolPoints[2];
        }
        else if (transform.position.x == patrolPoints[2].x && transform.position.z == patrolPoints[2].z)
        {
            curTar = patrolPoints[0];
        }
        
    }

    private void Chase()
    {
        _anim.PlayInFixedTime("Run_Weponless");
        _anim.speed = 0.9f;
        _navMeshAgent.speed = eneRunSpeed;
        Move(_player.transform.position);
        if (Vector3.Distance(this.transform.position, _player.transform.position) > chaseRadius)
        {
            state = "Search";
            lastPos = _player.transform.position;
            startTime = Time.time;
        }
        if (Vector3.Distance(this.transform.position, _player.transform.position) < AttRange)
        {
            state = "Attack";
        }
    }

    private void Search()
    {
        Move(lastPos);
        checkForPlayer();
        //Debug.Log("Time-Time = " + (Time.time - startTime) + ", threshold = " + threshTime + ", starTime = " + startTime);
        if(transform.position.x == lastPos.x && transform.position.z == lastPos.z)
        {
            _anim.PlayInFixedTime("Look_Around");
            if (Time.time - startTime > threshTime)
            {
                state = "Patrol";
            }
        }
        else
        {
            _anim.PlayInFixedTime("Run_Weponless");
            startTime = Time.time;
        }
        
    }

    private void Attack()
    {
        if (Vector3.Distance(this.transform.position, _player.transform.position) < AttRange)
        {
            GameManager.ResetGame();
        }
        else
        {
            state = "Chase";
        }
    }

    private void checkForPlayer()
    {
        if(Vector3.Distance(this.transform.position, _player.transform.position) < searchRadius)
        {
            lastPos = _player.transform.position;
            state = "Chase";
        }
    }

    private void checkForPlayerRun()
    {
        if (Vector3.Distance(this.transform.position, _player.transform.position) < searchRunRadius)
        {
            lastPos = _player.transform.position;
            state = "Chase";
        }
    }

    private void LookAt(Transform lookAtTransform)
    {
        _lookAtTransform = lookAtTransform;

        if (lookAtTransform == null)
            _navMeshAgent.updateRotation = true;
        else
            _navMeshAgent.updateRotation = false;
    }

    private void Turn()
    {
        if (_lookAtTransform != null)
        {
            Vector3 lookRotation = new Vector3((_lookAtTransform.position.x - transform.position.x), 1, (_lookAtTransform.position.z - transform.position.z));

            Quaternion newRotatation = Quaternion.LookRotation(lookRotation);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotatation, Time.fixedDeltaTime * turnSpeed);
        }
    }

    private void Move(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
    }

    private void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			GameManager.ResetGame();
            GameManager.StartGame();
		}
	}
    
    public void setPatrolPoint(Vector3[] patrolPoint)
    {
        patrolPoints = patrolPoint;
    }
}