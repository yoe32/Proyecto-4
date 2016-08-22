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

		}

		void EnemyDead ()
		{			
			decreaseShield (100);
			decreaseAllEnergy (100);

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

		public void decreaseAllEnergy(float ammount)
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

		void InTriggerStay(Collider collider)
		{
			if (collider.name == "Flamethrower(Clone)")
			{				
				if (enemySight.calculatePathLength (collider.gameObject.transform.position) <= enemy.GetComponent<SphereCollider> ().radius) 
				{
					if (curShield <= 0f)
						decreaseHealth (5f);
					else
						decreaseShield (5f);
				}
			}
		}

		void OnTriggerEnter(Collider collider)
		{		
			if (collider.name == "Proyectile_Bullet(Clone)") 
			{
				if (enemySight.calculatePathLength (collider.gameObject.transform.position) <= enemy.GetComponent<SphereCollider> ().radius) 
				{
					if (curShield <= 0f)
						decreaseHealth (5f);
					else
						decreaseShield (5f);
					Destroy (collider.gameObject);
				}

			}

			Debug.Log (collider.name);
			switch (collider.tag) 
			{
			case "Mine":
				{
					if (enemySight.calculatePathLength (collider.gameObject.transform.position) <= enemy.GetComponent<CapsuleCollider> ().radius)
					{
						if (curShield <= 0f) {
							decreaseHealth (5f);
						} else
							decreaseShield (5f);
					
						blueFireBall = Instantiate (blueFirePrefab, GetComponent<Transform> ().transform.position, GetComponent<Transform> ().transform.rotation) as GameObject;
						Destroy (collider.gameObject);
						Destroy (blueFireBall, 3);
					}
					break;
				}						
			
			case "Bullet":
				{ 
					if (enemySight.calculatePathLength (collider.gameObject.transform.position) <= enemy.GetComponent<CapsuleCollider> ().radius) 
					{
						decreaseHealth (5f);
						Destroy (collider.gameObject);
					}
						break;
					
				}
			}
		}
	}
}

