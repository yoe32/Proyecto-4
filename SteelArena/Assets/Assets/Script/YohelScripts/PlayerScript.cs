using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
	public Image healthBar;
	public Image energyBar;
	public Image shieldBar;
	float curHealth = 0f;
	float curEnergy = 0f;
	float curShield = 0f;
	float maxStat = 100f;
	Animator animator;
	Rigidbody rbody;
	float inputH;
	float inputV;

	// Use this for initialization
	void Start () 
	{
		curHealth = maxStat;
		curEnergy = maxStat;
		animator = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		//InvokeRepeating ("decreaseHealth", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (curHealth <= 0) 
		{
			animator.Play ("Death", -1, 0f);
		}

		if(Input.GetKeyDown("1"))
		{
			animator.Play ("Transform_to_Roller", -1, 0f);
		}

		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");

		animator.SetFloat ("inputH", inputH);
		animator.SetFloat ("inputV", inputV);

		float moveX = inputH * 20f * Time.deltaTime;
		float moveZ = inputV * 50f * Time.deltaTime;

		if (moveZ <= 3f) {
			moveX = 3f;
		} 

		rbody.velocity = new Vector3 (moveX, 0f, moveZ);
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

	void decreaseHealth()
	{
		if (curShield > 0) 
		{
			decreaseShield (5);
		} 
		else 
		{
			curHealth -= 5f;
			float calcHealth = curHealth / maxStat;
			setHealth (calcHealth);
		}
	}

	void decreaseEnergy(float ammount)
	{
		curEnergy -= ammount;
		float calcEnergy = curEnergy / maxStat;
		setEnergy (calcEnergy);
	}

	void decreaseShield(float ammount)
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

	void OnTriggerEnter(Collider target)
	{
		switch(target.tag)
		{
		case "Life":
			{
				increaseHealth (30);
				Destroy(target.gameObject);
				break;
			}
		case "Energy":
			{
				increaseEnergy (30);
				Destroy(target.gameObject);
				break;
			}
		case "Shield":
			{
				increaseShield (50);
				Destroy(target.gameObject);
				break;
			}
		case "Ammo":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Bullet":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Bomb":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Mine":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Fire":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Bonus_1":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Bonus_2":
			{
				Destroy(target.gameObject);
				break;
			}
		case "Bonus_3":
			{
				Destroy(target.gameObject);
				break;
			}
		case "L_Life":
			{
				increaseHealth (60);
				Destroy(target.gameObject);
				break;
			}
		case "L_Energy":
			{
				increaseEnergy (60);
				Destroy(target.gameObject);
				break;
			}
		case "L_Shield":
			{
				increaseShield (100);
				Destroy(target.gameObject);
				break;
			}
		case "L_Bonus_1":
			{
				Destroy(target.gameObject);
				break;
			}
		case "L_Bonus_2":
			{
				Destroy(target.gameObject);
				break;
			}
		case "L_Bonus_3":
			{
				Destroy(target.gameObject);
				break;
			}
		}
	}
}
