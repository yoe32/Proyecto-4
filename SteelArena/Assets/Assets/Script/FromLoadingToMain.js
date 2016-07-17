#pragma strict

	var speed: float;
	var countdown: float;

function Update () 
{
	countdown -= Time.deltaTime;

	if(countdown <= 0.0f)
	{
		SceneManagement.SceneManager.LoadScene("MainScene");
	}
}