﻿using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
public class EnemyAI : MonoBehaviour 
{
	public float patrolSpeed = 1f;                          // The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
	public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.


	private EnemySight enemySight;                          // Reference to the EnemySight script.
	private NavMeshAgent navMeshAgent;                               // Reference to the nav mesh agent.
	private Transform player;                               // Reference to the player's transform.
	private PlayerHealth playerHealth;                      // Reference to the PlayerHealth script.
	private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
	private float chaseTimer;                               // A timer for the chaseWaitTime.
	private float patrolTimer;                              // A timer for the patrolWaitTime.
	private int wayPointIndex;                              // A counter for the way point array.
	private GameObject[] bulletShooting;

	void Awake ()
	{
		// Setting up the references.
		enemySight = GetComponent<EnemySight>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
		bulletShooting = GameObject.FindGameObjectsWithTag("EnemyBullet");
	}


	void Update ()
	{
		// If the player is in sight and is alive...
			if(enemySight.playerInSight && playerHealth.curHealth > 0f)
			// ... shoot.
			Shooting();

		// If the player has been sighted and isn't dead...
			else if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.curHealth > 0f)
			// ... chase.
			Chasing();

		// Otherwise...
		else
			// ... patrol.
			Patrolling();
	}


	void Shooting ()
	{
			int i;
		// Stop the enemy where it is.
			navMeshAgent.Stop();

			for (i = 0; i < bulletShooting.Length; i++) 
			{
				//Instantiate Bullet
				bulletShooting[i].GetComponent<BulletShooting>().attack();
			}
	}


	void Chasing ()
	{
		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > 4f)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
				navMeshAgent.destination = enemySight.personalLastSighting;

		// Set the appropriate speed for the NavMeshAgent.
			navMeshAgent.speed = chaseSpeed;

		// If near the last personal sighting...
			if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
		{
			// ... increment the timer.
			chaseTimer += Time.deltaTime;

			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			chaseTimer = 0f;
	}


	void Patrolling ()
	{			
		// Set an appropriate speed for the NavMeshAgent.
			navMeshAgent.speed = patrolSpeed;

		// If near the next waypoint or there is no destination...
			if(navMeshAgent.destination == lastPlayerSighting.resetPosition || navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;

			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;

				// Reset the timer.
				patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;

		// Set the destination to the patrolWayPoint.
			navMeshAgent.destination = patrolWayPoints[wayPointIndex].position;
	}
}

}