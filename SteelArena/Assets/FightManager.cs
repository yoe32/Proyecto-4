using UnityEngine;
using System.Collections;

public class FightManager : MonoBehaviour {

	GameObject wrapper;
	GameObject mech;
	private bool hasAnimator;
	Animator animator;
	// Use this for initialization
	void Start () {

	

		if (!hasAnimator) {
		/*/	mech = GameObject.Find ("testerino");
			mech.transform.position = Vector3.zero;
			animator = mech.GetComponent<Animator> ();
			hasAnimator = true;
			wrapper = GameObject.Find ("MechWrapper");
			mech.transform.parent = wrapper.transform;
		*/
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
