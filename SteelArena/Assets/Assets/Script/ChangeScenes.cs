using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour {


	public void Load_CityScene()
	{
		SceneManager.LoadScene ("PauseMenuScreen");
	}

	public void LoadMainMenu()
	{
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
		SceneManager.LoadScene ("LoadingBeforeBattle");
	}
}
