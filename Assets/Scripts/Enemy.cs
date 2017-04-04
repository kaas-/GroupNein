using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float searchRadius;
    public float searchRunRadius;
    public float chaseRadius;
    public float turnSpeed;
    public float threshTime;
    public Vector3[] patrolPoints;

    private GameObject _player;
    private Transform _lookAtTransform;
    private string state = "Patrol";
    private float curRadius;
    private NavMeshAgent _navMeshAgent;
    private Vector3 curTar;
    private Vector3 lastPos;
    private float startTime;

    void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        curRadius = searchRadius;
        curTar = patrolPoints[0];
    }

    public void stateControl(GameObject player, bool isWalking)
    {
        _player = player;
        Turn();
        Move(curTar);
        if (state == "Patrol")
        {
            Patrol();
            checkForPlayer();
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
    }

    private void Patrol()
    {
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
        _navMeshAgent.speed = 8;
        Move(_player.transform.position);
        if (Vector3.Distance(this.transform.position, _player.transform.position) > chaseRadius)
        {
            _navMeshAgent.speed = 3.8f;
            state = "Search";
            lastPos = _player.transform.position;
            startTime = Time.time;
        }
    }

    private void Search()
    {
        Move(lastPos);
        checkForPlayer();
        Debug.Log("Time-Time = " + (Time.time - startTime) + ", threshold = " + threshTime + ", starTime = " + startTime);
        if(Time.time - startTime > threshTime)
        {
            state = "Patrol";
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

}
