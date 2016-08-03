using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour {

	GameObject splash;
	GameObject levelAudio;

	void Start()
	{		
		splash = GameObject.FindGameObjectWithTag("SplashAudio");
		levelAudio = GameObject.FindGameObjectWithTag ("LevelAudio");
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
