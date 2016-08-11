using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour 
{
	public float fieldOfViewAnglw = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;

	private NavMeshAgent navMeshAgent;
	private SphereCollider sphereCollider;
	private Animator animator;
	private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private Animator playerAnimator;
//	private PlayerHealth playerHealth;
	private HashIDs hash;
	private Vector3 previousSighting;

	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
		sphereCollider = GetComponent<SphereCollider> ();
		animator = GetComponent<Animator> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnimator = player.GetComponent<Animator> ();
	//	playerHealth = player.GetComponent<PlayerHealth> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();

		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}

	void Upgrade()
	{
		if (lastPlayerSighting.position != previousSighting)
			personalLastSighting = lastPlayerSighting.position;

		previousSighting = lastPlayerSighting.position;

	//	if (playerHealth.health > 0f)
	//		animator.SetBool (hash.playerInSightBool, playerInSight);
	//	else
	///		anim.SetBool (hash.playerInSightBool, false);
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			if (angle < fieldOfViewAnglw * 0.5f) 
			{
				RaycastHit hit;

				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
				{
					if(hit.collider.gameObject == player)
					{
						playerInSight = true;
						lastPlayerSighting.position = player.transform.position;
					}
				}
			}

			int playerLayerZeroStateHash = playerAnimator.GetCurrentAnimatorStateInfo (0).nameHash;
			int playerLayerOneStateHash = playerAnimator.GetCurrentAnimatorStateInfo (1).nameHash;

			if(playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shotState)
			{
				if(calculatePathLength(player.transform.position) <= sphereCollider.radius)
				{
					personalLastSighting = player.transform.position;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == player.tag)
			playerInSight = false;
	}

	float calculatePathLength(Vector3 targetPosition)
	{
		NavMeshPath path = new NavMeshPath ();

		if (navMeshAgent.enabled)
			navMeshAgent.CalculatePath (targetPosition, path);

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		allWayPoints [0] = transform.position;
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		for (int i = 0; i < path.corners.Length; i++) 
		{
			allWayPoints [i + 1] = path.corners [i];
		}

		float pathLength = 0f;

		for(int i = 0; i<allWayPoints.Length-1; i++)
		{
			pathLength += Vector3.Distance (allWayPoints [i], allWayPoints [i + 1]);
		}
		return pathLength;
	}
}
