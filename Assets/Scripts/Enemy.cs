﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.Player == "Player") {
			Debug.Log ("You Lost :(");
			GameManager.ResetLevel;

		}
		
	}

}
