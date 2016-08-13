﻿using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
public class EnemySight : MonoBehaviour 
{
		public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
		public bool playerInSight;                      // Whether or not the player is currently sighted.
		public Vector3 personalLastSighting;            // Last place this enemy spotted the player.

		private NavMeshAgent navMeshAgent;                       // Reference to the NavMeshAgent component.
		private SphereCollider sphereCollider;                     // Reference to the sphere collider trigger component.
		private Animator animator;                          // Reference to the Animator.
		private LastPlayerSighting lastPlayerSighting;  // Reference to last global sighting of the player.
		private GameObject player;                      // Reference to the player.
		private Animator playerAnimator;                    // Reference to the player's animator component.
		private PlayerHealth playerHealth;              // Reference to the player's health script.
		private HashIDs hash;                           // Reference to the HashIDs.
		private Vector3 previousSighting;               // Where the player was sighted last frame.

	void Awake()
	{
			// Setting up the references.
		navMeshAgent = GetComponent<NavMeshAgent> ();
		sphereCollider = GetComponent<SphereCollider> ();
		animator = GetComponent<Animator> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnimator = player.GetComponent<Animator> ();
		playerHealth = player.GetComponent<PlayerHealth> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();

			// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}

	void Upgrade()
	{
			// If the last global sighting of the player has changed...
		if (lastPlayerSighting.position != previousSighting)
				// ... then update the personal sighting to be the same as the global sighting.
			personalLastSighting = lastPlayerSighting.position;

			// Set the previous sighting to the be the sighting from this frame.
		previousSighting = lastPlayerSighting.position;

			// If the player is alive...
			if (playerHealth.curHealth > 0f)
				// ... set the animator parameter to whether the player is in sight or not.
			animator.SetBool (hash.playerInSightBool, playerInSight);
		else
				// ... set the animator parameter to false.
				animator.SetBool (hash.playerInSightBool, false);
	}

	void OnTriggerStay(Collider other)
	{
			// If the player has entered the trigger sphere...
		if (other.gameObject == player) 
		{
				// By default the player is not in sight.
			playerInSight = false;

				// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

				//Debug.Log ("angle: " + angle);
				Debug.Log ("fieldOfViewAngle: " + fieldOfViewAngle * 0.5f);
				// If the angle between forward and where the player is, is less than half the angle of view...
			if (angle < fieldOfViewAngle * 0.5f) 
				{					
				RaycastHit hit;

					// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
				{										
					if(hit.collider.gameObject == player)
					{
							// ... the player is in sight.
						playerInSight = true;
							// Set the last global sighting is the players current position.
						lastPlayerSighting.position = player.transform.position;
					}
				}
			}

				// Store the name hashes of the current states.
			int playerLayerZeroStateHash = playerAnimator.GetCurrentAnimatorStateInfo (0).nameHash;
			int playerLayerOneStateHash = playerAnimator.GetCurrentAnimatorStateInfo (1).nameHash;

				// If the player is running or is attracting attention...
				if(playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shotState)
			{
					// ... and if the player is within hearing range...
				if(calculatePathLength(player.transform.position) <= sphereCollider.radius)
				{
						// ... set the last personal sighting of the player to the player's current position.
					personalLastSighting = player.transform.position;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
			// If the player leaves the trigger zone...
		if (other.gameObject == player)
				// ... the player is not in sight.
			playerInSight = false;
	}

	float calculatePathLength(Vector3 targetPosition)
		{
			// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath ();

		if (navMeshAgent.enabled)
			navMeshAgent.CalculatePath (targetPosition, path);

			// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

			// The first point is the enemy's position.
		allWayPoints [0] = transform.position;

			// The last point is the target position.
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

			// The points inbetween are the corners of the path.
		for (int i = 0; i < path.corners.Length; i++) 
		{
			allWayPoints [i + 1] = path.corners [i];
		}

			// Create a float to store the path length that is by default 0.
		float pathLength = 0f;

			// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i<allWayPoints.Length-1; i++)
		{
			pathLength += Vector3.Distance (allWayPoints [i], allWayPoints [i + 1]);
		}
		return pathLength;
	}
}
}