using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {


	public Animator anim;
	public float inputH;
	public float inputV;
	public Rigidbody rigidBody;
	bool running, jump;
	// Use this for initialization
	void Start () {

		inputH   = 	Input.GetAxis ("Horizontal");
		inputV	 = 	Input.GetAxis ("Vertical");
		anim 	 =  GetComponent<Animator> ();

		anim.SetFloat ("InputV", inputV);
		anim.SetFloat ("InputH", inputH);

		rigidBody = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void Update () {



		inputH   = 	Input.GetAxis ("Horizontal");
		inputV	 = 	Input.GetAxis ("Vertical");


		float moveX = inputH * 20f * Time.deltaTime;
		float moveZ = inputV * 50f  * Time.deltaTime;



		anim.SetFloat ("InputV", inputV);
		anim.SetFloat ("InputH", inputH);

		if (moveZ <= 0f)
			moveX = 0f;
		else if (running) {
			moveX *= 3f;
			moveZ *= 3f;	
		}





		/*
		if (anim.GetCurrentAnimatorStateInfo (0).IsName("DAMAGED01") || anim.GetCurrentAnimatorStateInfo (0).IsName("DAMAGED00") ) {

			rigidBody.velocity = new Vector3(0,0,0);
		}
		else

			rigidBody.velocity = new Vector3(moveX, 0f, moveZ);
/*/

		/*
		if (Input.GetKey (KeyCode.LeftShift)) {
			running = true;
		} else {
			running = false;
		}


		if (Input.GetKey (KeyCode.Space)) {
			jump = true;
		} else {
			jump = false;
		}



		anim.SetBool ("run", running);
		anim.SetBool ("jump", jump);



		if (Input.GetKeyDown("1")){

			anim.Play ("WAIT01", -1, 0f);

		}
		if (Input.GetKeyDown("2")){

			anim.Play ("WAIT02", -1, 0f);

		}
		if (Input.GetKeyDown("3")){

			anim.Play ("WAIT03", -1, 0f);

		}
		if (Input.GetKeyDown("4")){

			anim.Play ("WAIT04", -1, 0f);

		}

		if (Input.GetMouseButtonDown(0)){

			anim.Play ("DAMAGED00", -1, 0f);

		}



		if (Input.GetMouseButtonDown(1)){

			anim.Play ("DAMAGED01", -1, 0f);

		}

		*/

	}

}
