using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class FinalInventory : MonoBehaviour {



	public MechManager manager;
	public Transform targetTransform;
	public List<string> mountNames;
	public GameObject mechItem;
	public List<GameObject> items;

	// Use this for initialization
	void Start () {


		manager = GameObject.Find ("Manager").GetComponent<MechManager> ();

		/*foreach (string s in mountNames) {

			GameObject x = Instantiate(mechItem);
			x.transform.SetParent(gameObject.transform);
			x.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			x.transform.position = Vector3.zero;
			x.transform.rotation = gameObject.transform.rotation;
			Text text = (x.GetComponentInChildren<Text>());



			items.Add(x);
			text.text = s;


		}*/


		gameObject.transform.localEulerAngles = new Vector3 (0, 20, 0);
		gameObject.transform.localPosition = new Vector3 (179, -154, -1548);

		//new Vector3 (179, -154, -1548); 
	}

	// Update is called once per frame
	void Update () {

	}



	public void deleteAll(){

		foreach(GameObject item in items){

		Destroy (item);
		}

	}


	public void makeAll(){


		foreach (string s in mountNames) {

			GameObject x = Instantiate(mechItem);
			x.transform.SetParent(gameObject.transform);
			x.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			x.transform.position = Vector3.zero;
			x.transform.rotation = gameObject.transform.rotation;
			Text text = (x.GetComponentInChildren<Text>());



			items.Add(x);
			text.text = s;
			Debug.Log (" EL nombre es " + text);


		}





	}

	public void makeAllByList(){

		deleteAll ();


		List<string> names = manager.getMountPointNames ();
		int position = 0;

		for (int y = 0; y < names.Count ; y++){

			GameObject x = Instantiate(mechItem);
			x.transform.SetParent(gameObject.transform);
			x.transform.localPosition = new Vector3 (0, 0, 174);
			x.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			x.transform.rotation = gameObject.transform.rotation;
			x.transform.name = position+"";
			Text text = (x.GetComponentInChildren<Text>());
			items.Add(x);
			text.text = names[y];
			//x.tag = "kek";



			x.GetComponentInChildren<Button> ().onClick.AddListener(() =>
				{
					manager._currentMount = Int32.Parse(x.transform.name);

					Debug.Log(" El current mountpoint es "  + manager._currentMount);
				});

			position++;

		}



	}



	public void setManagerMountPoint(int position){


		manager._currentMount = position;

	}







}
