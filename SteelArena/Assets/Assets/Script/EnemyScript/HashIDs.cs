using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour 
{

	public int dyingState;
	public int deadBool;
	public int locomotionState;
	public  int shotState;
	public int speedFloat;
	public int sneakingBool;
	public int shootingBool;
	public int playerInSightBool;
	public int shotFloat;
	public int aimWeightFloat;
	public int angularSpeedFloat;
	public int openBool;

	void Awake()
	{
		dyingState = Animator.StringToHash ("Base Layer.Dying");
		deadBool = Animator.StringToHash ("Dead");
		locomotionState = Animator.StringToHash ("Base Layer.Locomotion");
		shotState = Animator.StringToHash ("Shooting.Shoot");
		speedFloat = Animator.StringToHash ("Speed");
		sneakingBool = Animator.StringToHash ("Sneaking");
		shootingBool = Animator.StringToHash ("Shooting");
		playerInSightBool = Animator.StringToHash ("PlayerInSight");
		shotFloat = Animator.StringToHash ("Shot");
		aimWeightFloat = Animator.StringToHash ("AimWeight");
		angularSpeedFloat = Animator.StringToHash ("AngularSpeed");
		openBool = Animator.StringToHash ("Open");
	}

}
