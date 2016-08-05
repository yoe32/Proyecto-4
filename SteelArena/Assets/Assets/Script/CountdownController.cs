using UnityEngine;
using System.Collections;

public class CountdownController : MonoBehaviour {


	public bool counterDownDone = false;
	private GameObject cityAudio;

	// Use this for initialization
	void Start () 
	{
		cityAudio = GameObject.FindGameObjectWithTag ("CityAudio");
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
		cityAudio.GetComponent<AudioSource> ().Play ();
	}


	private void Continue()
	{
		
		Time.timeScale = 1;
	}

}
