using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{

	public Transform player;
	public float playerDistance;
	public float rotationDamping;
	public float chaseStartRange;
	public float moveSpeed;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		playerDistance = Vector3.Distance(player.position, transform.position);

		if (playerDistance < 500f)
		{			
			lookAtPlayer ();
		}
		if (playerDistance < 500f)		{			
			
			if (playerDistance > 10f) 
			{				
				chase ();
			}
			else if (playerDistance < 100f) 
			{
				//attack ();
			}
		}
	}

	void lookAtPlayer()
	{
		
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDamping);	
	}

	void chase()
	{
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}
	/*
	void attack()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) 
		{
			if (hit.collider.gameObject.tag == "Player") 
			{
				hit.collider.gameObject.GetComponent<PlayerHealth> ().DecreaseHealth ();
			}
		}
	}
	*/
}
