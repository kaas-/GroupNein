using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public GameObject WinningText
	public void Win()
	{
		WinningText.SetActive (true);
		GameManager.ResetLevel;
	}

	void OnTriggerEnter()
	{
		GameManager.instance.Win ();
	}



}
