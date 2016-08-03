using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Splash : MonoBehaviour {

	Image uiImage;
	Canvas parentCanvas;
	AudioSource deadmau5Audio;

	[SerializeField]
	Sprite[] images; // cantidad de imagenes

	[SerializeField]
	bool clickToProceed;

	[SerializeField]
	float fadeTime;

	[SerializeField]
	float displayTime;

	[SerializeField]
	float transparentTime;

	public float time = 4f; //30 seconds for you

	// Use this for initialization
	void Start () {
		parentCanvas = GetComponent<Canvas>();
		deadmau5Audio = parentCanvas.GetComponent<AudioSource> ();
		StopMusic();

		if (parentCanvas.worldCamera != Camera.main)
			parentCanvas.worldCamera = Camera.main;

		uiImage = GetComponentInChildren<Image>();

		uiImage.sprite = images[0];

		StartCoroutine(CycleImages());

	}
	// Update is called once per frame
	void Update () 
	{

	}

	IEnumerator CycleImages()
	{
		for (int i = 0; i < images.Length; i++)
		{
			if (i == 1) 
			{
				PlayMusic ();
				DontDestroyOnLoad (deadmau5Audio);
			}
			uiImage.sprite = images[i];
			uiImage.color = new Color(uiImage.color.r, uiImage.color.g, uiImage.color.b, 0);

			yield return new WaitForSeconds(transparentTime);

			//Bucle hace que aparezca de forma suave la imagen
			for (float j = 0; j < 1; j += Time.deltaTime / fadeTime)
			{
				uiImage.color = new Color(uiImage.color.r, uiImage.color.g, uiImage.color.b, j);

				yield return null; // Wait for frame then return to execution
			}

			yield return new WaitForSeconds(displayTime);

			//Bucle hace que desaparezca la imagen de forma suave 
			for (float j = 1; j > 0; j -= Time.deltaTime / fadeTime)
			{
				uiImage.color = new Color(uiImage.color.r, uiImage.color.g, uiImage.color.b, j);

				yield return null; // Wait for frame then return to execution
			}
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Application.LoadLevel ("LoadingScene");
	}
	void StopMusic()
	{
		deadmau5Audio.Stop();
	}
	void PlayMusic ()
	{
		deadmau5Audio.Play();
	}
}