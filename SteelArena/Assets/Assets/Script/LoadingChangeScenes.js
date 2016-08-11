#pragma strict

	var speed: float;
	var countdown: float;

function Update () 
{
	countdown -= Time.deltaTime;

	if(countdown <= 0.0f)
	{
		if(SceneManagement.SceneManager.GetActiveScene().name == "LoadingScene")
		SceneManagement.SceneManager.LoadScene("MainScene");
		if(SceneManagement.SceneManager.GetActiveScene().name == "LoadingBeforeBattle")
		SceneManagement.SceneManager.LoadScene("CityBattleScene");
		if(SceneManagement.SceneManager.GetActiveScene().name == "LoadingAfterBattle")
		SceneManagement.SceneManager.LoadScene("LevelScene");
	}
}