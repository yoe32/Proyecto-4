#pragma strict

import UnityEngine.EventSystems;

public var currentMount : Transform;
public var speedFactor : float = 0.1;



function Start () {

}

function Update ()
 {
	transform.position = Vector3.Lerp(transform.position,currentMount.position,speedFactor);
	transform.rotation = Quaternion.Slerp(transform.rotation,currentMount.rotation,speedFactor);
}

function setMount(newMount : Transform)
{	
	currentMount = newMount;
}

