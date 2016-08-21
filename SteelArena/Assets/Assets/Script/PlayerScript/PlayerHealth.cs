using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DigitalRuby.PyroParticles
{
	public class PlayerHealth : MonoBehaviour 
	{
		                      
		//public AudioClip deathClip;                         // The sound effect of the player dying.

		BattleController sphereController;

		public AudioClip mineClip;

		public GameObject prefabMine;
		private GameObject mine;
		private GameObject minesContainer;

		private GameObject fullMine;
		private GameObject orangeBar;
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
		public float curHealth = 0f;						// How much health the player has left.
		float curEnergy = 0f;
		public float curShield = 0f;
		float maxStat = 100f;

		private Animator animator;                              // Reference to the animator component.
		private PlayerMovement playerMovement;              // Reference to the player movement script.
		private HashIDs hash;                               // Reference to the HashIDs.
		private bool playerDead;                            // A bool to show if the player is dead or not.


		void Awake ()
		{
			// Setting up the references.
			animator = GetComponent<Animator>();
			playerMovement = GetComponent<PlayerMovement>();
			hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		}

		void Start()
		{
			curHealth = maxStat;
			curEnergy = maxStat;
			InvokeRepeating ("decreaseEnergy", 0f, 2f);

			totalAmount = 100f;
			speed = 20f;
			number = 5;

			minesContainer = GameObject.FindGameObjectWithTag (Tags.minesContainer);
			explotionContainer = GameObject.FindGameObjectWithTag(Tags.explotionContainer);
			fullMine = GameObject.FindGameObjectWithTag (Tags.fullMine);
			orangeBar = GameObject.FindGameObjectWithTag (Tags.orangeBar);
		}

		void Update ()
		{			
			// If health is less than or equal to 0...
			if(curHealth <= 0f)
			{				
				// ... and if the player is not yet dead...
				if (!playerDead)
					// ... call the PlayerDying function.
					PlayerDying ();
				else {
					// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
					PlayerDead ();
				}
			}

			if (number > 0)
			{
				orangeBar.GetComponent<Image> ().enabled = true;
				fullMine.GetComponent<Image> ().enabled = true;
			}
				
				
			if (Input.GetMouseButtonDown (1)) 
			{
				if (totalAmount > 0) 
				{					
					mine = Instantiate (prefabMine, minesContainer.transform.position, minesContainer.transform.rotation) as GameObject;
					totalAmount -= speed;
					number--;
					if (number <= 0) 
					{
						orangeBar.GetComponent<Image> ().enabled = false;
						fullMine.GetComponent<Image> ().enabled = false;
						number = 0;
					} 
				} 
			}
				textIndicator.GetComponent<Text> ().text = (number).ToString ();
				LoadingBar.GetComponent<Image> ().fillAmount =  totalAmount / 100;
		}

		void PlayerDying ()
		{
			// The player is now dead.
			playerDead = true;

			// Set the animator's dead parameter to true also.
			animator.SetBool(hash.deadBool, playerDead);

			// Play the dying sound effect at the player's location.
			//AudioSource.PlayClipAtPoint(deathClip, transform.position);		
		}

		void PlayerDead ()
		{
			// If the player is in the dying state then reset the dead parameter.
			if(animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState)
				animator.SetBool(hash.deadBool, false);

			// Disable the movement.
			animator.SetFloat(hash.speedFloat, 0f);
			playerMovement.enabled = false;

			// Stop the music playing.
			GetComponent<AudioSource>().Stop();
		}

		public void decreaseHealth()
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

		public void decreaseShield()
		{
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
		private void IncreaseMinesBar()
		{						
			totalAmount += speed;
			number++;

			if (number > 5)
				number = 5;
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
					Destroy (blueFireBall, 3);
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
			case "Dome":
				{
					AudioSource.PlayClipAtPoint(mineClip, transform.position, 1.0f);
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