using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DigitalRuby.PyroParticles;
//using UnityEditor;
using System;



/// comentario


public class MechManager : MonoBehaviour
{
	public List<MechPrefab> mechParts; //= new List<MechPrefab>();

	private MechType _mechType = MechType.Biped;
	private MechSize _mechSize = MechSize.Light;



	public bool isPlaying = false;
	private int _currentPrefab = 0;
	public int  _currentMount = 0;
	private bool firstTime = true;
	private bool fixedCam = false;
	bool isLocked = false;
	Animator animator;
	MechManager miniManager;

	private Vector3 curNormal = Vector3.up; // smoothed terrain normal
	private Quaternion iniRot; // initial rotation

	private List<MechPrefab> _prefabList = new List<MechPrefab>();

	private GameObject rootGo;
	private string _savedMech = string.Empty;

	public string MechData { get { return _savedMech; } }
	public MechType Type {   get { return _mechType; } }
	public MechSize Size {   get { return _mechSize; } }

	Vector3 cameraPos;

	public Image healthBar, energyBar, shieldBar;

	public MechPrefab CurrentPart
	{
		get
		{
			if(_prefabList.Count > 0)
			{
				return _prefabList[_currentPrefab];
			}

			return null;
		}
		set
		{
			_currentMount = 0;
			_currentPrefab = _prefabList.IndexOf(value);
		}
	}

	public int CurrentMount { get { return _currentMount; } }
	public List<MechPrefab> PrefabList { get { return _prefabList; } }

	public void UpdateGui()
	{
		//Debug.Log("Update your GUI here");
	}






	void Start(){
	//	Camera.main.transform.Rotate (35,0,0);
		///cameraPos =   new Vector3(50,30,10);
		/// 
		/// 
		if (isPlaying) {
			makeFromMemory ();
			Debug.Log ("EL ROOT GO ES " + rootGo);
			//rootGo.gameObject.transform.parent = GameObject.Find ("a").gameObject.transform;
			rootGo.gameObject.transform.localPosition.Set (190, 15, 500);
			rootGo.transform.position = new Vector3 (190, 15, 500);
			rootGo.gameObject.transform.position.Set (190, 15, 500);

		}



		/*prin
		Camera.main.transform.parent = rootGo.transform;
		//	Camera.main.transform.position = rootGo.transform.position;
		Camera.main.transform.position = new Vector3(0,10,-10);
		//Camera.main.transform.position = new Vector3(0, 15, -20);
		Camera.main.transform.Rotate (20,0,0);
		Camera.main.transform.parent = rootGo.transform;

/*/
			}


	void Update () {


		if(Input.GetKeyDown(KeyCode.P)){

			printOrder();


			}



		float inputH     = 	Input.GetAxis ("Horizontal");
		float inputV	 = 	Input.GetAxis ("Vertical");


		if (animator != null) {

			int moveX = checkMovementX ();
			int moveY = checkMovementY ();

			animator.SetFloat ("Horizontal", inputH);
			animator.SetFloat ("Vertical"  , inputV);

			if (!firstTime) {

				if (!fixedCam) {	

				//	Camera.main.transform.position = Vector3.zero;
					Camera.main.transform.parent = rootGo.transform;
				//	Camera.main.transform.position = rootGo.transform.position;
					Camera.main.transform.localPosition = new Vector3(0,10,-10);
					//Camera.main.transform.position = new Vector3(0, 15, -20);
					Camera.main.transform.Rotate (20,0,0);


					fixedCam = true;
				}
				Camera.main.transform.parent = rootGo.transform;

				//rootGo.GetComponent<Rigidbody> ().AddForce ();
				//rootGo.transform.position += Vector3.up;

			}

		}

		if (Input.GetKeyDown("1") && animator != null)

			animator.Play ("Death", -1 ,0f);

		if (Input.GetKeyDown ("2") && animator != null) {
			animator.SetBool ("isRolling", true);
			animator.Play ("Transform_to_Roller", -1, 0f);
		}
	

	}

	public int checkMovementX(){
		int result = 0;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			result += 1;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			result -= 1;
		}
		return result;
	}
	public int checkMovementY(){
		int result = 0;
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			result += 1;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			result -= 1;
		}
		return result;
	}




	public void SpawnPart(MechPrefab prefab)
	{

		int amount = 0;

		if(_prefabList.Count > 0)
		{
			foreach(Transform child in _prefabList[_currentPrefab].mounts[_currentMount].mountPoint)
			{
				MechPrefab oldPrefab = child.GetComponent<MechPrefab>();


				if(oldPrefab)
				{
					MechPrefab[] grandChildren = oldPrefab.GetComponentsInChildren<MechPrefab>();

					foreach(MechPrefab grandChild in grandChildren)
					{

					
						RemovePart(grandChild);
					}

					RemovePart(oldPrefab);
					Destroy(child.gameObject);
				}
			}
		}

		GameObject go = (GameObject)Instantiate(prefab.gameObject, new Vector3(0,0,0), Quaternion.identity);
		//go.transform.localScale += new Vector3 (1, 1, 1);

		if (firstTime) {
			firstTime = false;

			animator = go.GetComponent<Animator> ();
			rootGo = go;
			/*
			DigitalRuby.PyroParticles.FPCScript script;

			script = new DigitalRuby.PyroParticles.FPCScript ();



		//	rootGo.AddComponent<>();

			//script = rootGo.GetComponent<FPCScript>();

			script.healthBar = GameObject.Find ("HP_Full").GetComponent<Image>();
			script.LoadingBar = GameObject.Find ("LoadingBar").GetComponent<Transform>();
			script.energyBar = GameObject.Find ("Energy_Full").GetComponent<Image>();
			script.shieldBar = GameObject.Find ("Shield_Full").GetComponent<Image>();
			script.textIndicator = GameObject.Find ("MinesIndicator").GetComponent<Transform>();

			rootGo.AddComponent(script);
/*/

			}


		if(_prefabList.Count > 0)
		{
			go.transform.parent = _prefabList[_currentPrefab].mounts[_currentMount].mountPoint;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = new Vector3(1, 1, 1);
		}


		Debug.Log ("Monto el " + go.GetComponent<MechPrefab>().name + " y su mount es " + _currentMount );
		go.GetComponent<MechPrefab> ().mountedOn = _currentMount;
		AddPart(go.GetComponent<MechPrefab>());
		UpdateGui();
	}

	public void AddPart(MechPrefab part)
	{
		_prefabList.Add(part);

		if(part.mounts.Count > 0)
		{
			_currentMount = 0;

			if(!isLocked)
			_currentPrefab = _prefabList.Count - 1;

		
		}
	}

	public void RemovePart(MechPrefab part)
	{
		_prefabList.Remove(part);
	}

	public void setLocked(){

		if (!isLocked)
			isLocked = true;
		else
			isLocked = false;

	}

	public MechPrefab GetPart(MechMountType mountType)
	{
		foreach(MechPrefab part in mechParts)
		{
			if((part.mechType == _mechType || part.mechType == MechType.Any) &&
				(part.partSizes.Contains(_mechSize) || part.partSizes.Count == 0) &&
				(part.mountType == mountType))
			{
				return part;
			}
		}

		return null;
	}

	public List<MechPrefab> GetParts()
	{
		List<MechPrefab> parts = new List<MechPrefab>();

		if(CurrentPart.mounts.Count > 0)
		{
			MechMount mount = CurrentPart.mounts[_currentMount];

			foreach(MechPrefab part in mechParts)
			{
				/*
				if((part.mechType == _mechType || part.mechType == MechType.Any) &&
					(part.partSizes  .Contains(_mechSize) || part.partSizes.Count == 0) &&
					(part.mountableOn.Contains(CurrentPart.mountType) || part.mountableOn.Count == 0) &&
					(mount.mountTypes.Contains(part.mountType) || (part.isWeapon && mount.allowsWeapon)))
				{*/
					parts.Add(part);
				//}
			}
		}

		return parts;
	}



	public MechPrefab getMechPart(string name){

		MechPrefab mech = null;

		foreach (MechPrefab prefab in mechParts) {
			string a = name.Remove(name.Length-7).ToString();
			string b = prefab.gameObject.name.ToString();
			/*Debug.Log (a);
			Debug.Log (b);
			Debug.Log (a.Equals(b));
			*/
			if (a.Equals(b))
				return prefab;
		}
		return mech;

	}


	public void printOrder(){

		//FindChildRecursive (rootGo.transform);
		string receta = "";
		foreach (MechPrefab prefab in _prefabList) {
			Debug.Log (" EL NOMBRE ES  " + prefab.gameObject.name + " El mount es " + prefab.gameObject.transform.parent +  " y su mount es " + prefab );

			MechPrefab[] grandChildren = prefab.GetComponentsInChildren<MechPrefab>();
			foreach(MechPrefab grandChild in grandChildren)
			{		//Debug.Log (" EL Tiene un child de " + grandChild.gameObject.name);
				//Debug.Log (" La cantidad de childs es " + grandChildren.Length);			
			}
			//List<MechPrefab> c = GetParts();

		//	Debug.Log (" el que encontro es " + test);

			MechPrefab test = getMechPart(prefab.gameObject.name);





			/*

			if (miniManager == null) {
				GameObject x;
				x = GameObject.Find ("CopyCat");
				x.AddComponent<MechManager> ();
				miniManager = x.GetComponent<MechManager> ();
				//Debug.Log (" Se creo el minimanager" );

			}*/
			//miniManager.mechParts = mechParts;
			foreach (MechPrefab x in mechParts) {
//				manager.mechParts.Add (x);
			}
		


			//miniManager.SpawnPart(GetMechPart(test.gameObject.transform.name), prefab.mountedOn);


			receta += test.gameObject.transform.name + "," + prefab.mountedOn + "|";
		}

		PlayerPrefs.SetString("robotRecipe", receta);
		Debug.Log (receta);
	}

	public List<string> getMountPointNames(){

		List<string> mounts = new List<string>();

		if(CurrentPart != null)

		foreach (MechMount child in CurrentPart.mounts) {
	

			mounts.Add (child.mountPoint.name);

		}

		return mounts;

	}

	public void makeFromMemory(){

		string recipe = PlayerPrefs.GetString("robotRecipe");

		string[] parts = recipe.Split('|');

		foreach (string s in parts) {


			if (s.Length > 0) {
				string[] subparts = s.Split (',');


				Debug.Log (s);

				SpawnPart (GetMechPart (subparts [0]), Int32.Parse (subparts [1]));


			}
		}



	}


	public static Transform FindChildRecursive(Transform root) {


		if (root != null) {
			Debug.Log (root.name);


			Transform child = null;
			foreach (Transform t in root.GetComponentsInChildren<Transform>()) {
				if (t != null)
				if (t.childCount > 0) {
					//child = FindChildRecursive (t);

				}
				return null;
			}

			return null;


		} else
			return null;
	}



	public void ScrollMount(){


		
		//MechMount mount = CurrentPart.mounts[_currentMount];
		
		if(CurrentPart != null)
		if (CurrentPart.mounts.Count > 0) {

			int total = CurrentPart.mounts.Count-1;
			_currentMount++;


			if (_currentMount > total) {

				_currentMount = 0;

			}


		}


	}
	public void SetMount(int id)
	{
		_currentMount = id;
		UpdateGui();
	}

	public void SetType(MechType mechType)
	{
		_mechType = mechType;
	}

	public void NextType()
	{
		_mechType++;

		if(_mechType >= MechType.Any)
		{
			_mechType = 0;
		}

		UpdateGui();
	}

	public void PreviousType()
	{
		_mechType--;

		if(_mechType < 0)
		{
			_mechType = MechType.Any - 1;
		}

		UpdateGui();
	}

	public void NextSize()
	{
		_mechSize++;

		if(_mechSize >= MechSize.Any)
		{
			_mechSize = 0;
		}

		UpdateGui();
	}

	public void PreviousSize()
	{
		_mechSize--;

		if(_mechSize < 0)
		{
			_mechSize = MechSize.Any - 1;
		}

		UpdateGui();
	}

	public void Reset(){


		cameraPos.x = 0;
		cameraPos.y = 1;
		cameraPos.z = -10;
		Camera.main.transform.parent = null;
	//	Camera.main.transform.position = Vector3.zero;
		firstTime = true;
		fixedCam = false;



		foreach(MechPrefab prefab in _prefabList)
		{
			Destroy(prefab.gameObject);
		}

		_prefabList.Clear();
		_currentMount = 0;
		_currentPrefab = 0;
		_mechSize = MechSize.Light;
		_mechType = MechType.Biped;
		
		UpdateGui();
	}

	public MechPrefab GetMechPart(string partName)
	{
		foreach(MechPrefab prefab in mechParts)
		{
			if(prefab.name == partName)
			{
				return prefab;
			}
		}

		return null;
	}


	public void saveAndLeave(){

		printOrder ();	
		UnityEngine.SceneManagement.SceneManager.LoadScene("MoonSceneTest");




	}







	public void SpawnPart(MechPrefab prefab, int mount)
	{

		Debug.Log (" EL INT QUE LLEGO ES  " + mount);

		int amount = 0;

		if(_prefabList.Count > 0)
		{
			foreach(Transform child in _prefabList[_currentPrefab].mounts[mount].mountPoint)
			{
				MechPrefab oldPrefab = child.GetComponent<MechPrefab>();


				if(oldPrefab)
				{
					MechPrefab[] grandChildren = oldPrefab.GetComponentsInChildren<MechPrefab>();

					foreach(MechPrefab grandChild in grandChildren)
					{


						RemovePart(grandChild);
					}

					RemovePart(oldPrefab);
					Destroy(child.gameObject);
				}
			}
		}

		GameObject go = (GameObject)Instantiate(prefab.gameObject, new Vector3(0,0,0), Quaternion.identity);
		//go.transform.localScale += new Vector3 (1, 1, 1);

		if (firstTime) {
			firstTime = false;
			animator = go.GetComponent<Animator> ();
			rootGo = go;

			if (isPlaying) {
				GameObject.Find ("Canvas").GetComponentInChildren<MapCanvasController> ().playerTransform = rootGo.transform;
				PlayerScript playerScript = go.AddComponent<PlayerScript> ();
				//playerScript.
				playerScript.rbody = go.GetComponent<Rigidbody> ();
				playerScript.animator = animator;
				playerScript.shieldBar = shieldBar;
				playerScript.healthBar = healthBar;
				playerScript.energyBar = energyBar;
			}

		}


		if(_prefabList.Count > 0)
		{
			go.transform.parent = _prefabList[_currentPrefab].mounts[mount].mountPoint;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = new Vector3(1, 1, 1);
		}


		Debug.Log ("Monto el " + go.GetComponent<MechPrefab>().name + " y su mount es " + _currentMount );
		go.GetComponent<MechPrefab> ().mountedOn = _currentMount;
		AddPart(go.GetComponent<MechPrefab>());
		UpdateGui();
	}
}