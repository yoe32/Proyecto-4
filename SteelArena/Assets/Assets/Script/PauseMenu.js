#pragma strict
import UnityEngine.UI;

var ObjPause : GameObject;

function Start() 
{
	ObjPause.SetActive(false);
}

function Update() 
{
	if(Input.GetKeyDown(KeyCode.Escape))
	Cambio();
}
function Pausar()
{
	ObjPause.SetActive(true);
	Time.timeScale = 0;
}

function Continuar()
{
	ObjPause.SetActive(false);
	Time.timeScale = 1;
}
function Cambio()
{
 	if(Time.timeScale == 1)
 	Pausar();
 	else if(Time.timeScale == 0)
 	Continuar();
}

function LoadMainScene()
	{
	UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene"); 		
	}
	
