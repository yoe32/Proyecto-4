using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour{


	[SerializeField]
	Behaviour[] componentsToDisable;
	private Camera sceneCamera;



	void Start(){

		if (!isLocalPlayer) {
			for (int x = 0; x < componentsToDisable.Length; x++) {
				componentsToDisable[x].enabled = false;
			}


		} else {

			sceneCamera = Camera.main;
			if (sceneCamera != null)
				sceneCamera.gameObject.SetActive (false);
		}

	}


	void OnDisable(){
		if(sceneCamera != null)
			sceneCamera.gameObject.SetActive (true);

	}




}
