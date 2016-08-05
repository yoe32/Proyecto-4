using UnityEngine;
using System.Collections;

public class ControlCanvasMain : MonoBehaviour {

    public GameObject GPanelTrag;
    public GameObject GPanelCockpit;
    public GameObject GPanelShoulders;
    public GameObject GPanelBackpack;
    GameObject[] TagGlowingKey;
    public bool isTrigger = false;
    public bool vandera = true;
    // Use this for initialization
    void Start () {
        GPanelTrag.SetActive(false);
        GPanelCockpit.SetActive(false);
        GPanelShoulders.SetActive(false);
        GPanelBackpack.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
        if(isTrigger == true)
        {
            GPanelTrag.SetActive(vandera);
        }
	}

    public void GPanelCockpitActve()
    {
        vandera = false;
        GPanelCockpit.SetActive(true);
        GPanelShoulders.SetActive(false);
        GPanelBackpack.SetActive(false);
    }

    public void GPanelShouldersActve()
    {
        vandera = false;
        GPanelCockpit.SetActive(false);
        GPanelShoulders.SetActive(true);
        GPanelBackpack.SetActive(false);
    }

    public void GPanelBackpackActve()
    {
        vandera = false;
        GPanelCockpit.SetActive(false);
        GPanelShoulders.SetActive(false);
        GPanelBackpack.SetActive(true);
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
