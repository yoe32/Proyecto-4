using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sphere : MonoBehaviour {

    GameObject[] panelTop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //Debug.Log("entro al menu top");
        panelTop = GameObject.FindGameObjectsWithTag("PanelTop");
        foreach(GameObject i in panelTop)
        {
            i.GetComponent<Image>().enabled = true;
            i.GetComponent<Button>().interactable = true;
        }
    }
}
