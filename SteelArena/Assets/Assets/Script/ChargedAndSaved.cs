using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChargedAndSaved : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		if (MainMenu.charging) 
		{
			player.transform.position = new Vector3 (PlayerPrefs.GetFloat ("Player x"), 0, PlayerPrefs.GetFloat ("Player z"));

		}
	}
	
	public void Save()
	{
		PlayerPrefs.SetFloat ("Player x", player.transform.position.x);
		PlayerPrefs.SetFloat ("Player z", player.transform.position.z);
		//PlayerPrefs.SetInt("Nivel", player.GetComponent<ch>)
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("MainScene");
	}
}
