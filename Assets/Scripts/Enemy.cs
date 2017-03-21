using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("You Lost :(");
			GameManager.ResetGame();

		}
		
	}

}
