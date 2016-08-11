using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{

	public Transform player;
	public float playerDistance;
	public float rotationDamping;
	public float chaseStartRange;
	public float moveSpeed;
	private GameObject bulletShooting;
	void Start()
	{
		bulletShooting = GameObject.FindGameObjectWithTag("Bullet");
	}
	// Update is called once per frame
	void Update () 
	{
		playerDistance = Vector3.Distance(player.position, transform.position);

		if (playerDistance < 500f)
		{			
			lookAtPlayer ();
		}
		if (playerDistance < 500f)		
		{			
			
			if (playerDistance > 40f) 
			{		
				attack ();
				chase ();
			}
			}

			else if (playerDistance < 100f) 
			{
				
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

	void attack()
	{
		int contadorDeDisparos = 5;
		while (contadorDeDisparos > 0) 
		{
			//transform.position = transform.forward * 0;
			bulletShooting.GetComponent<BulletShooting>().attack();
			contadorDeDisparos--;
		}
	}
	
}
