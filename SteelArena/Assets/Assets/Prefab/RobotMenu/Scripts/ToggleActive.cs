using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToggleActive : MonoBehaviour {

    public GameObject Button1;
    bool newValue = false;

    public void ToggleView(bool newValue)
    {
		
		Debug.Log (newValue);

	

        if (newValue == false)
        {
			
            Button1.GetComponent<Button>().interactable = true;

        }
        else
        {
            Button1.GetComponent<Button>().interactable = false;
        }
    }
}
