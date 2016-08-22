using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ChangeScenes : MonoBehaviour {

	GameObject splash;
	GameObject levelAudio;
	public GameObject buttonClicked;

	void Start()
	{		
		splash = GameObject.FindGameObjectWithTag(Tags.splashAudio);
		levelAudio = GameObject.FindGameObjectWithTag (Tags.levelAudio);
	}

	public void LoadMainMenu()
	{		
		
		levelAudio.GetComponent<AudioSource> ().Stop ();
		splash.GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene ("MainScene");
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void LoadRobotMenu()
	{
		Debug.Log ("buttonClicked: "+ EventSystem.current.currentSelectedGameObject.tag);
		buttonClicked = GameObject.FindGameObjectWithTag (EventSystem.current.currentSelectedGameObject.tag);
		DontDestroyOnLoad (levelAudio);
		SceneManager.LoadScene ("LabRobotMenu");
	}
	public void LoadingBeforeBattle()
	{
		levelAudio.GetComponent<AudioSource> ().Stop ();
		SceneManager.LoadScene ("LoadingBeforeBattle");
	}

	public void LoadingAfterBattle()
	{		
		SceneManager.LoadScene ("LoadingAfterBattle");
	}
}
