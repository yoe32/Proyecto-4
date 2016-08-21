using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DigitalRuby.PyroParticles
{
	public class PlayerHealth : MonoBehaviour 
	{
		                      
		//public AudioClip deathClip;                         // The sound effect of the player dying.
		private EnemySight enemySight;

		BattleController sphereController;
		private GameObject enemy;
		public Transform explotionPrefab;

		private GameObject secondPlayerCamera;

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
			enemy = GameObject.FindGameObjectWithTag (Tags.enemy);
			secondPlayerCamera = GameObject.FindGameObjectWithTag (Tags.secondPlayerCamera);
			enemySight = enemy.GetComponent<EnemySight> ();
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
			fullMine = GameObject.FindGameObjectWithTag (Tags.fullMine);
			orangeBar = GameObject.FindGameObjectWithTag (Tags.orangeBar);
		}

		void Update ()
		{			
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

		void PlayerDead ()
		{
			// Stop the music playing.
		//	GetComponent<AudioSource>().Stop();

			secondPlayerCamera.GetComponent<Camera> ().enabled = true;

			// Disable the movement.
			animator.SetFloat(hash.speedFloat, 0f);
			playerMovement.enabled = false;

			// Stop the music playing.
			GetComponent<AudioSource>().Stop();

			enemySight.playerInSight = false;

			secondPlayerCamera.transform.parent = orangeBar.transform;

			blueFireBall = Instantiate (explotionPrefab, GetComponent<Transform> ().transform.position, GetComponent<Transform> ().transform.rotation) as GameObject;
			AudioSource.PlayClipAtPoint (mineClip, transform.position, 1.0f);
			Destroy (this.gameObject);
		}

		public void decreaseHealth()
		{			
			curHealth -= 5f;
			float calcHealth = curHealth / maxStat;
			setHealth (calcHealth);
			if (curHealth <= 0f)
				PlayerDead ();
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
			
			if (collider.name == "Flamethrower(Clone)" && curShield <= 0f)
			{
				if (curShield <= 0f)
					decreaseHealth ();
				else
					decreaseShield ();

			}
			switch (collider.tag) 
			{

			case "Mine":
				{
					if (curShield <= 0f) 
					{
						decreaseHealth ();
					} 
					else
						decreaseShield ();

					blueFireBall = Instantiate (blueFirePrefab, GetComponent<Transform>().transform.position,GetComponent<Transform>().transform.rotation) as GameObject;
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
			case "Enemy":
				{
					if (enemySight.calculatePathLength(this.gameObject.transform.position) <= enemy.GetComponent<SphereCollider>().radius * 0.5 ) 
					{
						secondPlayerCamera.GetComponent<Camera> ().enabled = true;
						secondPlayerCamera.transform.parent = orangeBar.transform;

						enemySight.playerInSight = false;

						Destroy (collider.gameObject);
						blueFireBall = Instantiate (explotionPrefab, GetComponent<Transform>().transform.position,GetComponent<Transform>().transform.rotation) as GameObject;
						Destroy (this.gameObject);
						AudioSource.PlayClipAtPoint(mineClip, transform.position, 1f);
					}
					break;
				}
			case "EnemyBullet":
				{ Debug.Log("bullet destroyed");
					Destroy (collider);
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

			switch (collider.tag) 
			{
			case "Dome":
				{					
					secondPlayerCamera.GetComponent<Camera> ().enabled = true;
					secondPlayerCamera.transform.parent = orangeBar.transform;

					enemySight.playerInSight = false;

					blueFireBall = Instantiate (explotionPrefab, GetComponent<Transform>().transform.position,GetComponent<Transform>().transform.rotation) as GameObject;
					AudioSource.PlayClipAtPoint(mineClip, transform.position, 1.0f);
					Destroy (this.gameObject);
					Destroy (blueFireBall, 3);
					break;
				}
			}
		}

	}

}