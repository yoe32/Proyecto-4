using UnityEngine;
using System.Collections;

public class ControlCanvasMain : MonoBehaviour {

    public GameObject GPanelTrag;
    public GameObject GPanelCockpit;
    public GameObject GPanelShoulders;
    public GameObject GPanelBackpack;
    public GameObject GPanelGun;
    GameObject[] TagGlowingKey;
    public bool isTrigger = false;
    public bool vandera = true;
    // Use this for initialization
    void Start () {
        GPanelTrag.SetActive(false);
        GPanelCockpit.SetActive(false);
        GPanelShoulders.SetActive(false);
        GPanelBackpack.SetActive(false);
        GPanelGun.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
        if(isTrigger == true)
        {
            GPanelTrag.SetActive(vandera);
        }
	}

    public void TouchButtomItem(string point)
    {
        if(point == "Mount_cockpit" || point == "Mount_Weapon_top" || point == "Mount_Weapon_Main"
            || point == "Top")
        {
            vandera = false;
            GPanelCockpit.SetActive(false);
            GPanelShoulders.SetActive(true);
            GPanelBackpack.SetActive(false);
            GPanelGun.SetActive(false);
        }
        //else if(point == "Mount_backpack" && point == "Mount_Backpack")
        //{
        //    vandera = false;
        //    GPanelCockpit.SetActive(false);
        //    GPanelShoulders.SetActive(true);
        //    GPanelBackpack.SetActive(false);
        //    GPanelGun.SetActive(false);
        //}
        else if(point == "Mount_backpack" || point == "Mount_Backpack")
        {
            vandera = false;
            GPanelCockpit.SetActive(false);
            GPanelShoulders.SetActive(false);
            GPanelBackpack.SetActive(true);
            GPanelGun.SetActive(false);
        }
        else if (point == "Mount_Weapon_L" || point == "Mount_Weapon_R" || point == "Mount_Shoulder_rockets_lvl2_L"
            || point == "Mount_Shoulder_rockets_lvl2_R" || point == "Mount_Shoulder_rockets_lvl1_L"
            || point == "Mount_Shoulder_rockets_lvl1_R" || point == "Mount_HalfShoulder_L" || point == "Mount_HalfShoulder_R")
        {
            vandera = false;
            GPanelCockpit.SetActive(false);
            GPanelShoulders.SetActive(false);
            GPanelBackpack.SetActive(false);
            GPanelGun.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "MainCamera")
        {
            isTrigger = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "MainCamera")
        {
            isTrigger = false;
        }
    }
}
