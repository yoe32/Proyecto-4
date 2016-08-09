﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	// Fix a range how early u want your enemy detect the obstacle.
	private int range;
	private float speed;
	private bool isThereAnyThing = false;
	// Specify the target for the enemy.
	public GameObject target;
	private float rotationSpeed;
	private RaycastHit hit;
	// Use this for initialization
	void Start() {
		range = 110;
		speed = 10f;
		rotationSpeed = 25f;
	}
	// Update is called once per frame
	void Update() {
		//Look At Somthly Towards the Target if there is nothing in front.
		if (!isThereAnyThing) {
			Vector3 relativePos = target.transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
		}
		// Enemy translate in forward direction.
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		//Checking for any Obstacle in front.
		// Two rays left and right to the object to detect the obstacle.
		Transform leftRay = transform;
		Transform rightRay = transform;
		//Use Phyics.RayCast to detect the obstacle
		if (Physics.Raycast(leftRay.position + (transform.right * 10), transform.forward, out hit, range) ||
			Physics.Raycast(rightRay.position - (transform.right * 10), transform.forward, out hit, range)) {
			if (hit.collider.gameObject.CompareTag("Obstacles")) {
				isThereAnyThing = true;
				transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
			}
		}
		// Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
		// Just making this boolean variable false it means there is nothing in front of object.
		if (Physics.Raycast(transform.position - (transform.forward * 10), transform.right, out hit, 65) ||
			Physics.Raycast(transform.position - (transform.forward * 10), -transform.right, out hit, 65)) {
			if (hit.collider.gameObject.CompareTag("Obstacles")) {
				isThereAnyThing = false;
			}
		}
		// Use to debug the Physics.RayCast.
		Debug.DrawRay(transform.position + (transform.right * 7), transform.forward * 30, Color.red);
		Debug.DrawRay(transform.position - (transform.right * 7), transform.forward * 30, Color.red);
		Debug.DrawRay(transform.position - (transform.forward * 4), -transform.right * 30, Color.yellow);
		Debug.DrawRay(transform.position - (transform.forward * 4), transform.right * 30, Color.yellow);
	}
}