using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DigitalRuby.PyroParticles
{
	public class BattleController : MonoBehaviour 
		{
			
			private GameObject[] flamethrowerList = new GameObject[4];
			private bool enabled;
			private bool combatFinished;
			private GameObject instructionsCanvas;
			private GameObject numberCounterInitBattle;
			private GameObject[] robotBars = new GameObject[1];
			int i;


		// Use this for initialization
			void Start () 
			{			
				instructionsCanvas = GameObject.FindGameObjectWithTag ("Instructions");
				numberCounterInitBattle = GameObject.FindGameObjectWithTag ("NumberCounter");
				robotBars = GameObject.FindGameObjectsWithTag ("RobotBars");
				flamethrowerList = GameObject.FindGameObjectsWithTag ("Flame");
				StartCoroutine (FlameTimer ());	
			}

			void Update()
			{
				
			}

			public void AfterAreYouReadyButtonIsPressed()
			{
				instructionsCanvas.SetActive (false);
				numberCounterInitBattle.GetComponent<Image> ().enabled = true;
				numberCounterInitBattle.GetComponent<Animator> ().enabled = true;

				for (i = 0; i <= robotBars.Length-1; i++) 
				{
					robotBars [i].GetComponent<Canvas> ().enabled = true;
				}
			}


			public IEnumerator FlameTimer()
		{		
				
				int randomPosition = Random.Range (0, 5);
				int randomCounter = Random.Range (3, 5);
				GameObject fireClone;
				int i = 0;
				int counter = 1000;
				yield return new WaitForSeconds (randomCounter + 5);

				while (counter > 0) 
				{

					i++;
					
					if (i >= randomCounter) 
					{
						yield return new WaitForSeconds (randomCounter);
						fireClone = Instantiate (flamethrowerList [randomPosition]) as GameObject;
						fireClone.GetComponent<FireBaseScript> ().Duration = 5.0f;
						Destroy (fireClone, 7);

						randomPosition = Random.Range (0, 5);
						randomCounter = Random.Range (3, 5);
						i = 0;
						//combatFinished = true;
					}

					counter--;
				}		
		}

			public void makeEnemyAppear()
			{
				GameObject[] enemyList = GameObject.FindGameObjectsWithTag ("Enemy");
				int i;
				int j;
				
				for(i = 0; i <= enemyList.Length;i++)
				{
					if(enemyList [i].GetComponent<EnemyHealth> ().curHealth > 0f) 
					{
						for (j = i+1; i <= enemyList.Length; i++) 
						{
							enemyList [i].SetActive(false);
						}

					}
					
				}				
			}

			public IEnumerator makeSphereAppearTimer(MeshRenderer sphereMesh)
			{		
				int random = Random.Range (10, 15);
				enabled = false;
				int memoryRandomNumber = random;
					
				Debug.Log (random);

				while(enabled == false) 
					{		
					
					random--;
											
						if (random <= 0) 
						{			
							yield return new WaitForSeconds (memoryRandomNumber);
							sphereMesh.enabled = true;							
							enabled = true;

						}

					}

				}
					
			}

	}
