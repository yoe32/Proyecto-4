#pragma strict

var floatup;

function Start () 
{
	floatup = false;

}

function Update () 
{
	if(floatup)
		sphereFloatingUp();
	else if(!floatup)
		floatingDown();
	
}
function sphereFloatingUp()
{
	transform.position.y += 0.9 * Time.deltaTime;
	yield WaitForSeconds(1);
	floatup = false;
}
function floatingDown()
{
	transform.position.y -= 0.9 * Time.deltaTime;
	yield WaitForSeconds(1);
	floatup = true;
}

