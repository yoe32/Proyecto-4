using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
public class EnemyShooting : MonoBehaviour 
{	
	public AudioClip shotClip;                          // An audio clip to play when a shot happens.

	private Animator animator;                              // Reference to the animator.
	private HashIDs hash;                               // Reference to the HashIDs script.	
	private SphereCollider sphereCollider;                         // Reference to the sphere collider.
	private Transform player;                           // Reference to the player's transform.
	private PlayerHealth playerHealth;                  // Reference to the player's health.
	private bool shooting;      
	private NavMeshAgent navMeshAgent; 
	private GameObject[] bulletShooting;					// A bool to say whether or not the enemy is currently shooting.

	void Awake ()
	{
		// Setting up the references.
		animator = GetComponent<Animator>();		
		sphereCollider = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.gameObject.GetComponent<PlayerHealth>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		bulletShooting = GameObject.FindGameObjectsWithTag("EnemyBullet");
		navMeshAgent = GetComponent<NavMeshAgent>();
	}


	void Update ()
	{			
		// Cache the current value of the shot curve.
			float shot = animator.GetFloat(hash.shotFloat);

		// If the shot curve is peaking and the enemy is not currently shooting...
			if (shot > 0.5f && !shooting) 
			{
				// ... shoot
				Shoot ();
			}
		// If the shot curve is no longer peaking...
		if(shot < 0.5f)
			{
				
			// ... the enemy is no longer shooting 
			shooting = false;
			
			}
	}


	void OnAnimatorIK (int layerIndex)
	{
		// Cache the current value of the AimWeight curve.
			float aimWeight = animator.GetFloat(hash.aimWeightFloat);

		// Set the IK position of the right hand to the player's centre.
			animator.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up);

		// Set the weight of the IK compared to animation to that of the curve.
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}


	void Shoot ()
	{
		
			int i;

		// The enemy is shooting.
		shooting = true;
		
			for (i = 0; i < bulletShooting.Length; i++) 
			{
				//Instantiate Bullet
				bulletShooting[i].GetComponent<BulletShooting>().attack();
			}
		

		// The fractional distance from the player, 1 is next to the player, 0 is the player is at the extent of the sphere collider.
			float fractionalDistance = (sphereCollider.radius - Vector3.Distance(transform.position, player.position)) / sphereCollider.radius;
				
		// The player takes damage.
		playerHealth.decreaseHealth();

		
	}


	
}

}