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
		splash = GameObject.FindGameObjectWithTag(Tags.splashAudio);
	}

	public void NewGame()
	{
<<<<<<< HEAD
		splash.GetComponent<Canvas> ().enabled = false;
		splash.GetComponent<AudioSource> ().Stop ();
=======

		if (splash != null){
			splash.GetComponent<Canvas> ().enabled = false;
			splash.GetComponent<AudioSource> ().Stop ();
	}
>>>>>>> 78b2814960075e12c6f98b92cd0981514494a120
		charging = false;
		SceneManager.LoadScene ("LevelMenu");
	}

	public void Continue()
	{
		charging = true;
		//SceneManager.LoadScene ("CityBattleScene");		

	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}

	