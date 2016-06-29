using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour 
{
	public float rotationSpeed = 125.0f;

	//private Vector3 pos1 = new Vector3(15,35f,10);
	//private Vector3 pos2 = new Vector3(15,35.2f,10);

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * 1);//rotates coin on Z axis
			//transform.position = Vector3.Lerp (pos1, pos2, (Mathf.Sin(2f * Time.time) + 1.0f) / 2.0f);
			//Debug.Log("You are rotating on the Z axis");
	}
}