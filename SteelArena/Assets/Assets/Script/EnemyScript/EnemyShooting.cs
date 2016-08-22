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
		private GameObject playerInSight;

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
			playerInSight = GameObject.FindGameObjectWithTag ("Enemy");
		}


		void Update ()
		{			

				// Cache the current value of the shot curve.
				float shot = animator.GetFloat(hash.shotFloat);

				// If the shot curve is peaking and the enemy is not currently shooting...
				if (playerInSight.GetComponent<EnemySight> ().playerInSight == true && !shooting) 
				{
					// ... shoot
					StartCoroutine(Shoot ());
				}
				// If the shot curve is no longer peaking...
				if(playerInSight.GetComponent<EnemySight>().playerInSight == false)
				{
					// ... the enemy is no longer shooting and disable the line renderer.
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


			IEnumerator Shoot ()
		{		
				int i;
				int shotCounter = 5;

					// The enemy is shooting.
					shooting = true;
			
					// Display the shot effects.
					ShotEffects ();

					while (shotCounter > 0) 
				{				
					
						for (i = 0; i < bulletShooting.Length; i++)
						{
							if (playerHealth.curHealth > 0f) 
							{
								//Instantiate Bullet
								bulletShooting [i].GetComponent<BulletShooting> ().attack ();
							}
							yield return new WaitForSeconds (0.1f);
						}

						// The player takes damage.
						if (playerHealth.curShield > 0f)
							playerHealth.decreaseShield (5f);
						else
							playerHealth.decreaseHealth (5f);
											
						shotCounter--;						
				}

			yield return new WaitForSeconds (5f);

			shooting = false;

				if (playerHealth.curHealth > 0f) 
				{
					// The fractional distance from the player, 1 is next to the player, 0 is the player is at the extent of the sphere collider.
					float fractionalDistance = (sphereCollider.radius - Vector3.Distance (transform.position, player.position)) / sphereCollider.radius * 10;
				}

		}

			void ShotEffects ()
			{			

				float volume = 2f;
				// Play the gun shot clip at the position of the muzzle flare.
							AudioSource.PlayClipAtPoint(shotClip, player.transform.position, volume);
			}
					
	}

}