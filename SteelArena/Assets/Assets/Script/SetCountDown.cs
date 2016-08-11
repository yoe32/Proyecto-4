﻿using UnityEngine;
using System.Collections;

public class SetCountDown : MonoBehaviour {

	private CountdownController countdownController;

	public void SetCountDownNow()
	{
		countdownController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<CountdownController> ();
		countdownController.counterDownDone = true;
	}

	void Update()
	{
		
	}
}
