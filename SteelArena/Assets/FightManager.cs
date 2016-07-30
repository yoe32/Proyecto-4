using UnityEngine;
using System.Collections;

public class FightManager : MonoBehaviour {

	GameObject game;
	private bool hasAnimator;
	Animator animator;
	// Use this for initialization
	void Start () {

	

		if (!hasAnimator) {
			game = GameObject.Find ("testerino");
			game.transform.position = Vector3.zero;
			animator = game.GetComponent<Animator> ();
			hasAnimator = true;
		}




	
	}
	
	// Update is called once per frame
	void Update () {


		float inputH     = 	Input.GetAxis ("Horizontal");
		float inputV	 = 	Input.GetAxis ("Vertical");


		if (animator != null) {

			animator.SetFloat ("Horizontal", inputH);
			animator.SetFloat ("Vertical", inputV);




		}
	
	}
}
