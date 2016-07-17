using UnityEngine;
using System.Collections;

public class CountdownController : MonoBehaviour {


	public bool counterDownDone = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (counterDownDone == false)
			Pause ();
		else
			Continue();
	}

	private void Pause()
	{		
		Time.timeScale = 0;
	}


	private void Continue()
	{
		
		Time.timeScale = 1;
	}

}
