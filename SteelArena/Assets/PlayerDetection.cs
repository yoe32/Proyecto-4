using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour 
{
	private GameObject player;                          // Reference to the player.
	private LastPlayerSighting lastPlayerSighting;      // Reference to the global last sighting of the player.


	void Awake ()
	{
		// Setting up references.
		player = GameObject.FindGameObjectWithTag(Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
	}


	void OnTriggerStay(Collider other)
	{		
			// ... and if the colliding gameobject is the player...
		if(other.gameObject == player)
			// ... set the last global sighting of the player to the colliding object's position.
			lastPlayerSighting.position = other.transform.position;
	}

}
