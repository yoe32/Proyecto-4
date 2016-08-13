using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour 
{
	public Vector3 position = new Vector3 (1000f, 1000f, 1000f);
	public Vector3 resetPosition = new Vector3(1000f,1000f,1000f);
	private GameObject player;


	void Start()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		position = player.transform.position;
	}
}


