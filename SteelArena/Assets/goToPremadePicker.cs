using UnityEngine;
using System.Collections;

public class goToPremadePicker : MonoBehaviour {

	// Use this for initialization
	public void goTo(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("PickerScene");
	}
}
