using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	private void Win()
	{
        Debug.Log("You win!");
		GameManager.ResetGame();
	}

	void OnTriggerEnter(Collider col)
	{
		Win();
	}



}
