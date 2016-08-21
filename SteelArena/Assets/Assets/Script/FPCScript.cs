using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DigitalRuby.PyroParticles
{
	public class FPCScript : MonoBehaviour 
	{
		BattleController sphereController;

		public GameObject prefabMine;
		private GameObject mine;
		private GameObject minesContainer;

		private GameObject orangeLoadingBar;
		public Transform LoadingBar;
		public Transform textIndicator;

		public GameObject blueFirePrefab;
		private GameObject blueFireBall;
		private GameObject explotionContainer;

		private float totalAmount;
		private float speed;
		private int number;

		public Image healthBar;
		public Image energyBar;
		public Image shieldBar;
		float curHealth = 0f;
		float curEnergy = 0f;
		float curShield = 0f;
		float maxStat = 100f;

		void Start()
		{
			totalAmount = 100f;
			speed = 20f;
			number = 5;

			curHealth = maxStat;
			curEnergy = maxStat;
			InvokeRepeating ("decreaseEnergy", 0f, 2f);


			minesContainer = GameObject.FindGameObjectWithTag (Tags.minesContainer);
			explotionContainer = GameObject.FindGameObjectWithTag(Tags.explotionContainer);
 		
		}
		void Update()
		{

			if (curHealth <= 0) 
			{
				Destroy (this.gameObject);
			}

			if(totalAmount <= 0)
				StartCoroutine(BlinkingLightMinesBar ());

			if (Input.GetMouseButtonDown (1)) 
			{
				
				if (totalAmount > 0) 
				{					
					mine = Instantiate (prefabMine, minesContainer.transform.position, minesContainer.transform.rotation) as GameObject;
					totalAmount -= speed;
					number--;
					if (number <= 0)
						number = 0;
				} 
			}

			textIndicator.GetComponent<Text> ().text = (number).ToString ();
			LoadingBar.GetComponent<Image> ().fillAmount =  totalAmount / 100;

		}

		void decreaseHealth()
		{			
				curHealth -= 5f;
				float calcHealth = curHealth / maxStat;
				setHealth (calcHealth);
		}

		void decreaseEnergy()
		{			
				curEnergy -= 2f;
				float calcEnergy = curEnergy / maxStat;
				setEnergy (calcEnergy);
		}

		void decreaseShield()
		{
			Debug.Log ("Shield");

			curShield -= 5f;
			float calcShield = curShield / maxStat;
			setShield (calcShield);
		}

		void increaseHealth(float ammount)
		{
			curHealth += ammount;

			if(curHealth > 100f)
			{
				curHealth = maxStat;
			}

			float calcHealth = curHealth / maxStat;
			setHealth (calcHealth);
		}

		void increaseEnergy(float ammount)
		{
			curEnergy += ammount;

			if(curEnergy > 100f)
			{
				curEnergy = maxStat;
			}

			float calcEnergy = curEnergy / maxStat;
			setEnergy (calcEnergy);
		}

		void increaseShield(float ammount)
		{
			curShield += ammount;

			if(curShield > 100f)
			{
				curShield = maxStat;
			}

			float calcShield = curShield / maxStat;
			setShield (calcShield);
		}

		void setHealth(float health)
		{
			healthBar.fillAmount = health;
		}

		void setEnergy(float energy)
		{
			energyBar.fillAmount = energy;
		}

		void setShield(float shield)
		{
			shieldBar.fillAmount = shield;
		}

		private void IncreaseMinesBar()
		{						
			totalAmount += speed;
			//orangeLoadingBar.SetActive (true);
			number++;

			if (number > 5)
				number = 5;
		}

		private IEnumerator BlinkingLightMinesBar()
		{					
			while (totalAmount <= 0) 
			{						
				//orangeLoadingBar.SetActive (false);
				yield return new WaitForSeconds (3);
			}
		}

		void OnTriggerEnter(Collider collider)
		{	
			
			if (collider.name == "Flamethrower(Clone)" && curShield <= 0) 
			{
				decreaseHealth ();
			}
			else
				decreaseShield ();
			
			switch (collider.tag) 
			{

			case "Mine":
				{
					if (curShield <= 0) 
					{
						decreaseHealth ();
					} 
					else
						decreaseShield ();
					
					blueFireBall = Instantiate (blueFirePrefab, explotionContainer.transform.position, explotionContainer.transform.rotation) as GameObject;
					Destroy (collider.gameObject);
					Destroy (blueFireBall, 1);
					break;
				}
			case "MinesBonus":
				{
					collider.GetComponent<MeshRenderer> ().enabled = false;
					IncreaseMinesBar ();
					break;
				}
			case "HealthBonus":
				{		
					collider.GetComponent<MeshRenderer> ().enabled = false;
					increaseHealth (30);
					break;
				}
			case "EnergyBonus":
				{
					collider.GetComponent<MeshRenderer> ().enabled = false;
					increaseEnergy (20);
					break;
				}
			case "ShieldBonus":
				{	
					collider.GetComponent<MeshRenderer> ().enabled = false;
					setShield (1);
					curShield = maxStat;								
					break;
				}
			}
		}

		void  OnTriggerExit(Collider collider)
		{
			if (collider.tag == "MinesBonus" || collider.tag == "HealthBonus" || collider.tag == "EnergyBonus" || collider.tag == "ShieldBonus") 
			{
				sphereController = new BattleController ();

				StartCoroutine (sphereController.makeSphereAppearTimer (collider.GetComponent<MeshRenderer> ()));
			}
		}
	}
}
