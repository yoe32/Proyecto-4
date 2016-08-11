using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
public class EnemyShooting : MonoBehaviour 
{	
	public AudioClip shotClip;                          // An audio clip to play when a shot happens.

	private Animator anim;                              // Reference to the animator.
	private HashIDs hash;                               // Reference to the HashIDs script.	
	private SphereCollider col;                         // Reference to the sphere collider.
	private Transform player;                           // Reference to the player's transform.
	private PlayerHealth playerHealth;                  // Reference to the player's health.
	private bool shooting;                              // A bool to say whether or not the enemy is currently shooting.
	private GameObject bulletShooting;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();		
		col = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.gameObject.GetComponent<PlayerHealth>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		bulletShooting = GameObject.FindGameObjectWithTag("Bullet");
	}


	void Update ()
	{
		// Cache the current value of the shot curve.
		float shot = anim.GetFloat(hash.shotFloat);

		// If the shot curve is peaking and the enemy is not currently shooting...
		if(shot > 0.5f && !shooting)
			// ... shoot
			Shoot();

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
		float aimWeight = anim.GetFloat(hash.aimWeightFloat);

		// Set the IK position of the right hand to the player's centre.
		anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up);

		// Set the weight of the IK compared to animation to that of the curve.
		anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}


	void Shoot ()
	{
		// The enemy is shooting.
		shooting = true;
		
		//Instantiate Bullet
		bulletShooting.GetComponent<BulletShooting>().attack();

		// The fractional distance from the player, 1 is next to the player, 0 is the player is at the extent of the sphere collider.
		float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
				
		// The player takes damage.
		playerHealth.decreaseHealth();

		
	}


	
}

}