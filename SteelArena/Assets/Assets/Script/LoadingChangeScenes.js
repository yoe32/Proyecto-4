#pragma strict

	var speed: float;
	var countdown: float;

function Update () 
{
	countdown -= Time.deltaTime;

	if(countdown <= 0.0f)
	{
		if(Application.loadedLevelName == "LoadingScene")
		SceneManagement.SceneManager.LoadScene("MainScene");
		if(Application.loadedLevelName == "LoadingBeforeBattle")
		SceneManagement.SceneManager.LoadScene("CityBattleScene");
		if(Application.loadedLevelName == "LoadingAfterBattle")
		SceneManagement.SceneManager.LoadScene("LevelScene");
	}
}