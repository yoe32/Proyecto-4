using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
public class EnemyAnimation : MonoBehaviour 
{

	public float deadZone = 5f;					// The number of degrees for which the rotation isn't controlled by Mecanim.

	private Transform player;
	private EnemySight enemySight;
	private NavMeshAgent navMeshAgent;               // Reference to the nav mesh agent.
	private Animator animator;                  // Reference to the Animator.
	private HashIDs hash;                   // Reference to the HashIDs script.
	private AnimatorSetup animatorSetup;        // An instance of the AnimatorSetup helper class.


	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		enemySight = GetComponent<EnemySight>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

		// Making sure the rotation is controlled by Mecanim.
		navMeshAgent.updateRotation = true;

		// Creating an instance of the AnimatorSetup class and calling it's constructor.
		animatorSetup = new AnimatorSetup(animator, hash);

		// Set the weights for the shooting and gun layers to 1.
		animator.SetLayerWeight(1, 1f);		

		// We need to convert the angle for the deadzone from degrees to radians.
		deadZone *= Mathf.Deg2Rad;
	}


	void Update () 
	{
		//this.GetComponent<Animator>().SetFloat("Speed", navMeshAgent.desiredVelocity.magnitude);
		// Calculate the parameters that need to be passed to the animator component.
		NavAnimSetup();
	}


	void OnAnimatorMove ()
	{	
		// Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.	
		//navMeshAgent.velocity = animator.deltaPosition / Time.deltaTime;
			this.GetComponent<Animator>().SetFloat("Speed", navMeshAgent.desiredVelocity.magnitude);

		// The gameobject's rotation is driven by the animation's rotation.
		transform.rotation = animator.rootRotation;
	}


	void NavAnimSetup ()
	{
		// Create the parameters to pass to the helper function.
		float speed;
		float angle;

		// If the player is in sight...
			if (enemySight.playerInSight) 
			{				

				// ... the enemy should stop...
				speed = 0f;

				// ... and the angle to turn through is towards the player.
				angle = FindAngle (transform.forward, player.position - transform.position, transform.up);
			}
		else
		{				
			// Otherwise the speed is a projection of desired velocity on to the forward vector...
			speed = Vector3.Project(navMeshAgent.desiredVelocity, transform.forward).magnitude;

			// ... and the angle is the angle between forward and the desired velocity.
			angle = FindAngle(transform.forward, navMeshAgent.desiredVelocity, transform.up);

			// If the angle is within the deadZone...
			if(Mathf.Abs(angle) < deadZone)
			{
				// ... set the direction to be along the desired direction and set the angle to be zero.
				transform.LookAt(transform.position + navMeshAgent.desiredVelocity);
				angle = 0f;
			}
		}

		// Call the Setup function of the helper class with the given parameters.
		animatorSetup.Setup(speed, angle);
	}


	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;

		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle(fromVector, toVector);
		
		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross(fromVector, toVector);

		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));

		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;


		return angle;
	}
}

}