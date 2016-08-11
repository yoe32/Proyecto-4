using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public AudioClip shotingClip;
	public AudioSource audio;
	public float turnSmooting = 15f;
	public float speedDampTime = 0.1f;

	private Animator animator;
	private HashIDs hash;

	void Awake()
	{
		animator = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent < HashIDs> ();
		animator.SetLayerWeight (1, 1f);
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak");

		MovementManagement (horizontal, vertical, sneak);
	}

	void Update()
	{
		bool shot = Input.GetKeyDown (KeyCode.Space);
		animator.SetBool (hash.shootingBool, shot);
		AudioManagement (shot);
	}

	void MovementManagement(float horizontal, float vertical, bool sneaking)
	{
		animator.SetBool (hash.sneakingBool, sneaking);

		if (horizontal != 0f || vertical != 0f) {
			Rotating (horizontal, vertical);
			animator.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
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

	void AudioManagement(bool shot)
	{
		if (animator.GetCurrentAnimatorStateInfo (0).nameHash == hash.shotState) 
		{
			if (!audio.isPlaying) 
			{
				audio.Play ();
			}
		} 
		else 
		{
			audio.Stop ();
		}

		if (shot) 
		{
			AudioSource.PlayClipAtPoint (shotingClip, transform.position);
		}
	}
}
