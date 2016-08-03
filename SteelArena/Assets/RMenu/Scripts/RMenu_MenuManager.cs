using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
[RequireComponent(typeof(RMenu_CanvasManager))]
public class RMenu_MenuManager : MonoBehaviour
{
    private enum SwipeDirection                                     //the directions that we can swipe in
    {
        Up = 0,
        Down,
        Right,
        Left
    }

	GameObject levelAudio;
	GameObject startButton;
	Button levelButton;
	public int actualLevelInMenu;
	string gameObjectTag;

    public float minSwipeDistY = 50f;                               //the minimum distance acceptable in the Y direction to trigger a swipe
    public float minSwipeDistX = 50f;                               //the minimum distance acceptable in the X direction to trigger a swipe
    public float swipeMaxDelay = .8f;                               //the maximum acceptable time between "down" and "up" to trigger a swipe

    private Vector2 swipeStartPos = Vector2.zero;                   //the position where the press or click started
    private float swipeStartTime = 0f;                              //recorded time when the press or click occurred

    public List<Transform> menuScreens;                             //list of menus we can turn through- add to list in the inspector
    private int currentMenuSlot = 0;
    private int currentMenuScreen = 0;

    public int maxTurnQueue = 2;                                    //max number of turns that can be queued at once with subsequent swipes
    public float secondsToTurn = 1f;                                //time to turn 90 degrees

    private bool turning = false;                                   //whether we're currently turning
    private int turnsLeft = 0;                                      //the number of turns left until the goal

    private RMenu_CanvasManager canvasManager;

    private void Awake()
    {
		levelAudio = GameObject.FindGameObjectWithTag ("LevelAudio");
		levelAudio.GetComponent<AudioSource> ().Play ();
		DontDestroyOnLoad (levelAudio);

		startButton = GameObject.FindGameObjectWithTag ("StartButton");
		startButton.GetComponent<Button> ().interactable = true;

		if (levelButton == null) 
		{
			levelButton = GameObject.Find ("Level1").GetComponent<Button> ();
			actualLevelInMenu = 1;
		}
		
        canvasManager = GetComponent<RMenu_CanvasManager>();

        if (menuScreens.Count < 2)
        {
            Debug.Log("At least 2 menus are required for the RMenu_MenuManager component to function. Disabling component.");

            gameObject.SetActive(false);
            return;
        }
    }

    private void OnEnable()
    {
        currentMenuSlot = 0;
        currentMenuScreen = 0;
        turning = false;
        turnsLeft = 0;

        //enable only the front menu
        menuScreens.ForEach(t => t.gameObject.SetActive(false));
        menuScreens[0].gameObject.SetActive(true);
    }

    private void Update()
    {
		
		if (Input.GetMouseButtonDown (0))
		{
			GameObject temp = EventSystem.current.currentSelectedGameObject;
			if (temp != null && temp.CompareTag("Level")) 
			{
				levelButton = GameObject.Find (EventSystem.current.currentSelectedGameObject.name).GetComponent<Button> ();
				turnsLeft = LevelRotationCounter ();
				Debug.Log (turnsLeft);
			}
		}


		if (!turning && turnsLeft != 0)
		{
			StartCoroutine ("RotateCamera");
		}
    }

	public int LevelRotationCounter()
	{
		
		int buttonLevel = levelNumber ();
		int positionCounter = 0;

		Debug.Log (actualLevelInMenu);

		if (actualLevelInMenu < buttonLevel) {
			Debug.Log ("nivel que se pide menos el actual");
			positionCounter = buttonLevel - actualLevelInMenu;
		} else if (actualLevelInMenu > buttonLevel) 
		{		
			Debug.Log ("nivel actual menos el que se pide");
			positionCounter = -(actualLevelInMenu - buttonLevel);

		}
		actualLevelInMenu = buttonLevel;
		return positionCounter;

	}

	private int levelNumber()
	{
		string lvlOneText = "Level1";
		string lvlTwoText = "Level2";
		string lvlThreeText = "Level3";
		int number = 0;


			for(int i = 0; i < levelButton.name.Length; i++)
			{
			Debug.Log (levelButton.name);
			if (string.Equals (levelButton.name [i], lvlOneText [i])) 
			{
				Debug.Log ("devuelve 1");
				number = 1;
			} else if (string.Equals (levelButton.name [i], lvlTwoText [i])) 
			{
				Debug.Log ("devuelve 2");
				number = 2;
			} else if (string.Equals (levelButton.name [i], lvlThreeText [i])) 
			{
				Debug.Log ("devuelve 3");
				number = 3;					
			}
			}

		return number;
		
	}

    private IEnumerator RotateCamera()
    {
        turning = true;
        canvasManager.menuSlotRaycasters.ToList().ForEach(p => p.enabled = false);

        while (turnsLeft != 0)
        {
            int direction = 1 * (int)Mathf.Sign(turnsLeft);

            AttachScreenToNextSlot(direction);

            Quaternion startRotation = canvasManager.cameraTransform.rotation;
            Quaternion endRotation = canvasManager.cameraTransform.rotation * Quaternion.Euler(0, direction * 90, 0);

            float rate = 1.0f / secondsToTurn;
            float t = 0.0f;

            while (t < 1.0f)
            {
                t += Time.unscaledDeltaTime * rate;
                canvasManager.cameraTransform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

				//change start button interactable to false when the camera changes position
				startButton.GetComponent<Button> ().interactable = false;

                yield return null;
            }

            turnsLeft -= 1 * direction;
        }

        canvasManager.menuSlotRaycasters[currentMenuSlot].enabled = true;
		startButton.GetComponent<Button> ().interactable = true;
        turning = false;
    }

    private void AttachScreenToNextSlot(int direction)
    {
        //force direction to -1, +1
        direction = 1 * (int)Mathf.Sign(direction);

        //get rough idea of next slot index
        int nextSlotIndex = currentMenuSlot + direction;

        //wrap around bounds, zero and total number of menus being used
        while (nextSlotIndex < 0)
            nextSlotIndex += canvasManager.menuCanvasses.Length;
        while (nextSlotIndex >= canvasManager.menuCanvasses.Length)
            nextSlotIndex -= canvasManager.menuCanvasses.Length;

        //get rough idea of next screen index
        int nextScreenIndex = currentMenuScreen + direction;

        //wrap around bounds, zero and the total number of screens being used (should be 4)
        while (nextScreenIndex < 0)
            nextScreenIndex += menuScreens.Count;
        while (nextScreenIndex >= menuScreens.Count)
            nextScreenIndex -= menuScreens.Count;

        //set parent of next screen to next slot
        menuScreens[nextScreenIndex].SetParent(canvasManager.menuRectTransforms[nextSlotIndex], false);

        //iterate through all screens and make sure only the current and next screen are enabled
        for (int i = 0; i < menuScreens.Count; i++)
        {
            if (i == currentMenuScreen || i == nextScreenIndex)
            {
                menuScreens[i].gameObject.SetActive(true);
            }
            else
            {
                menuScreens[i].gameObject.SetActive(false);
            }
        }

        //assign next indices to current indices
        currentMenuScreen = nextScreenIndex;
        currentMenuSlot = nextSlotIndex;
    }

    private void OnSwipe(SwipeDirection direction)
    {
        if (direction == SwipeDirection.Left)
        {
            if (turnsLeft < maxTurnQueue)
                turnsLeft += 1;
			
        }
        else if (direction == SwipeDirection.Right)
        {
            if (turnsLeft > -1 * maxTurnQueue)
                turnsLeft -= 1;
        }
    }

    private void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))        //first clicked
        {			
            swipeStartPos = Input.mousePosition;
            swipeStartTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))     //click ended
        {
            //if the swipe took too long, don't bother, also if there was an input break in the middle (like running off the screen, alt+tabbing, etc...)
            if (Time.time <= swipeStartTime + swipeMaxDelay && swipeStartPos != Vector2.zero)
            {
                float swipeDistVertical = (new Vector3(0, Input.mousePosition.y, 0) - new Vector3(0, swipeStartPos.y, 0)).magnitude;
                float swipeDistHorizontal = (new Vector3(Input.mousePosition.x, 0, 0) - new Vector3(swipeStartPos.x, 0, 0)).magnitude;

                //vertical distance greater than horizontal distance, and vertical distance greater than distance requirement
                if (swipeDistVertical > swipeDistHorizontal && swipeDistVertical > minSwipeDistY)
                {
                    float swipeValue = Mathf.Sign(Input.mousePosition.y - swipeStartPos.y);

                    if (swipeValue > 0)                         //up-swipe
                    {
                        OnSwipe(SwipeDirection.Up);
                    }
                    else if (swipeValue < 0)                    //down-swipe
                    {
                        OnSwipe(SwipeDirection.Down);
                    }
                }
                //horizontal distance greater than vertical distance, and horizontal distance greater than distance requirement
                else if (swipeDistHorizontal > swipeDistVertical && swipeDistHorizontal > minSwipeDistX)
                {
                    float swipeValue = Mathf.Sign(Input.mousePosition.x - swipeStartPos.x);

                    if (swipeValue > 0)                         //right-swipe
                    {
                        OnSwipe(SwipeDirection.Right);
                    }
                    else if (swipeValue < 0)                    //left-swipe
                    {
                        OnSwipe(SwipeDirection.Left);
                    }
                }
            }
        }
        else if (!Input.GetMouseButton(0))      //no input
        {
            swipeStartPos = Vector2.zero;
        }
    }
}
