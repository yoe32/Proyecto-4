using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
	public static bool charging;
	public Button button;

	void Start()
	{
		
		button.interactable = PlayerPrefs.HasKey("Player z");

	}

	public void NewGame()
	{
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

	