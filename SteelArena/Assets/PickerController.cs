using UnityEngine;
using System.Collections;

public class PickerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public void chooseType(int x ){


		switch (x) {

		case 0:
			PlayerPrefs.SetString("robotRecipe", "Legs_Spider_Lt,0|Shoulders_Med_Cobra,0|Cockpit_Gun,4|Weapon_DoubleGun_lvl5,3|Backpack_Med_Frame,2|");
			break;
		case 1:
			PlayerPrefs.SetString("robotRecipe", "Legs_Spider_Med,0|Shoulders_Med_Tank,0|Weapon_Sniper_lvl2,0|Weapon_Sniper_lvl2,1|Cockpit_Gun_Flat_Full,4|Backpack_Tank_Hvy,2|Weapon_DoubleGun_lvl1,3|");
			break;

		case 2:
			PlayerPrefs.SetString("robotRecipe", "Legs_Spider_Hvy,0|Cockpit_Gun_Flat_Part,0|Weapon_Sniper_lvl3,0|Weapon_DoubleGun_lvl3,1|Weapon_DoubleGun_lvl2,3|Backpack_Tank_Lt,2|");
			break;


		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("MoonSceneTest");


	}
}
