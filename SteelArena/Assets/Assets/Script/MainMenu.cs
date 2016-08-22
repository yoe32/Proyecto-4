using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
	public static bool charging;
	GameObject splash;

	void Start()
	{
		splash = GameObject.FindGameObjectWithTag("SplashAudio");
	}

	public void NewGame()
	{

		if (splash != null){
			splash.GetComponent<Canvas> ().enabled = false;
			splash.GetComponent<AudioSource> ().Stop ();
	}
		charging = false;
		SceneManager.LoadScene ("LevelScene");
	}

	public void Continue()
	{
		charging = true;
		SceneManager.LoadScene ("CityBattleScene");		

	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}

	