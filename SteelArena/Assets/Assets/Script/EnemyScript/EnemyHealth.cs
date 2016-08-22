using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DigitalRuby.PyroParticles
{
	public class EnemyHealth : MonoBehaviour 
	{

		//public AudioClip deathClip;                         // The sound effect of the player dying.
		private EnemySight enemySight;

		BattleController sphereController;
		private GameObject enemy;

		private GameObject secondPlayerCamera;

		public AudioClip mineClip;

		public GameObject prefabMine;
		private GameObject mine;
		private GameObject minesContainer;

		private GameObject enemyFullMine;
		private GameObject enemyRedBar;
		public Transform LoadingBar;
		public Transform textIndicator;

		public GameObject blueFirePrefab;
		private GameObject blueFireBall;

		private float totalAmount;
		private float speed;
		private int number;

		//Player Bars
		public Image healthBar;
		public Image energyBar;
		public Image shieldBar;
		public float curHealth = 0f;						// How much health the player has left.
		private float curEnergy = 0f;
		public float curShield = 0f;
		private float maxStat = 100f;

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
			enemyFullMine = GameObject.FindGameObjectWithTag (Tags.enemyFullMine);
			enemyRedBar = GameObject.FindGameObjectWithTag (Tags.enemyRedBar);
		}

		void Update ()
		{	
			if (number > 0)
			{
				enemyRedBar.GetComponent<Image> ().enabled = true;
				enemyFullMine.GetComponent<Image> ().enabled = true;
			}			

			StartCoroutine (throwMine ());

		}

		void EnemyDead ()
		{			
			decreaseShield (100);
			decreaseAllEnergy (100);
			decreaseHealth (100);

			blueFireBall = Instantiate (blueFirePrefab, GetComponent<Transform> ().transform.position, GetComponent<Transform> ().transform.rotation) as GameObject;
			AudioSource.PlayClipAtPoint (mineClip, transform.position, 1.0f);
			Destroy (this.gameObject);


		}

		public void decreaseHealth(float ammount)
		{						
			if (curShield > 0) 
			{
				decreaseShield (ammount);
			} else 
			{
				curHealth -= ammount;
				float calcHealth = curHealth / maxStat;
				setHealth (calcHealth);

				if (curHealth <= 0f)
					EnemyDead ();
			}
		}

		void decreaseEnergy()
		{
			curEnergy -= 5f;
			float calcEnergy = curEnergy / maxStat;
			setEnergy (calcEnergy);
		}

		void decreaseAllEnergy(float ammount)
		{
			curEnergy -= ammount;
			float calcEnergy = curEnergy / maxStat;
			setEnergy (calcEnergy);
		} 

		public void decreaseShield(float ammount)
		{
			curShield -= ammount;
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

		IEnumerator throwMine()
		{
			int random = Random.Range (1000, 1500);

			yield return new WaitForSeconds (random);

			if (totalAmount > 0) 
			{					
				mine = Instantiate (prefabMine, minesContainer.transform.position, minesContainer.transform.rotation) as GameObject;
				totalAmount -= speed;
				number--;
				if (number <= 0) 
				{
					enemyRedBar.GetComponent<Image> ().enabled = false;
					enemyFullMine.GetComponent<Image> ().enabled = false;
					number = 0;
				} 
			}

			textIndicator.GetComponent<Text> ().text = (number).ToString ();
			LoadingBar.GetComponent<Image> ().fillAmount =  totalAmount / 100;
		}
		void InTriggerStay(Collider collider)
		{
			if (collider.name == "Flamethrower(Clone)" && curShield <= 0f)
			{
				if (curShield <= 0f)
					decreaseHealth (5f);
				else
					decreaseShield (5f);

			}
		}

		void OnTriggerEnter(Collider collider)
		{		
			switch (collider.tag) 
			{
			case "Mine":
				{
					if (curShield <= 0f) 
					{
						decreaseHealth (10f);
					} 
					else
						decreaseShield (10f);

					blueFireBall = Instantiate (blueFirePrefab, GetComponent<Transform>().transform.position,GetComponent<Transform>().transform.rotation) as GameObject;
					Destroy (collider.gameObject);
					Destroy (blueFireBall, 3);
					break;
				}						
			
			case "Bullet":
				{ 
					decreaseHealth (10f);
					Destroy (collider.gameObject);
					break;
				}
			}
		}
	}
}

