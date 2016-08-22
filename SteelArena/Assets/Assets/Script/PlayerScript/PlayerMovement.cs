using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{	
	public float turnSmooting = 15f;
	public float speedDampTime = 0.1f;
	private GameObject[] bulletShooting;
	private GameObject countdownController;
	private Rigidbody rigidbody;
	private Animator animator;
	private HashIDs hash;

	void Awake()
	{
		animator = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent < HashIDs> ();
		bulletShooting = GameObject.FindGameObjectsWithTag("PlayerBullet");
		countdownController = GameObject.FindGameObjectWithTag (Tags.gameController);
		rigidbody = GetComponent<Rigidbody> ();
		animator.SetLayerWeight (1, 1f);
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak");

		if(Input.GetKeyDown("1"))
		{
			animator.Play ("Transform_to_Roller", 3);
		}

		MovementManagement (horizontal, vertical, sneak);
	}

	void Update()
	{
		if (countdownController.GetComponent<CountdownController>().counterDownDone == true && Input.GetMouseButtonDown (0)) 
		{
			attack ();
		}

	}

	void MovementManagement(float horizontal, float vertical, bool sneaking)
	{
		
			animator.SetBool (hash.sneakingBool, sneaking);
					

		if (horizontal != 0f || vertical != 0f) 
		{				
				Rotating (horizontal, vertical);
				animator.SetFloat (hash.speedFloat, 3.0f, speedDampTime, Time.deltaTime);
		} 
		else 
		{
			animator.SetFloat (hash.speedFloat, 0f);
		}

	}

	void Rotating(float horizontal, float vertical)
	{
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody> ().rotation, targetRotation, turnSmooting * Time.deltaTime);
		GetComponent<Rigidbody> ().MoveRotation (newRotation);
	}
	void attack()
	{		
		int i;
		for(i = 0; i < bulletShooting.Length; i++)
			bulletShooting[i].GetComponent<BulletShooting>().attack();
			
		}


	}


