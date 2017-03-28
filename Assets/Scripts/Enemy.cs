using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float searchRadius;
    public float chaseRadius;
    public float turnSpeed;
    public Vector3[] patrolPoints;


    private Transform _lookAtTransform;
    private string state = "Patrol";
    private float curRadius;
    private NavMeshAgent _navMeshAgent;
    public Vector3 curTar;

    void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        curRadius = searchRadius;
        curTar = patrolPoints[0];
    }

    public void stateControl(GameObject player)
    {
        Turn();
        Move(curTar);
        if (state == "Patrol")
        {
            Patrol();
            checkForPlayer(player);
        }
        else if(state == "Chase")
        {
            Chase(player);
        }
        else if(state == "Search")
        {
            checkForPlayer(player);
            
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

    private void Chase(GameObject player)
    {
        _navMeshAgent.speed = 8;
        Move(player.transform.position);
        if (Vector3.Distance(this.transform.position, player.transform.position) > chaseRadius)
        {
            _navMeshAgent.speed = 3.8f;
            state = "Patrol";
        }
    }

    private void checkForPlayer(GameObject player)
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < searchRadius)
        {
            //Move(player.transform.position);
            state = "Chase";
        }
    }

    private void Search()
    {

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
