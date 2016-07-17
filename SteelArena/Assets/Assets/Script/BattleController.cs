using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.PyroParticles
{
public class BattleController : MonoBehaviour 
	{
		
		private GameObject[] flamethrowerList = new GameObject[4];
		private bool enabled;
		private bool combatFinished;

	// Use this for initialization
	void Start () 
		{			
				
				flamethrowerList = GameObject.FindGameObjectsWithTag ("Flame");
				StartCoroutine (FlameTimer ());

		}


		public IEnumerator FlameTimer()
	{		
			
			int randomPosition = Random.Range (0, 6);
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

					randomPosition = Random.Range (0, 6);
					randomCounter = Random.Range (3, 5);
					i = 0;
					//combatFinished = true;
				}

				counter--;


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
