using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuPopUpControl : MonoBehaviour {

    public GameObject GOScroll;
    public GameObject ControlScroll;
    public GameObject Screen;
    //public GameObject GameObjectPos;
    public GameObject[] PanControl;
    public GameObject[] PanelAnim;
    public GameObject[] groupButton;
    Button[] btnHijoPanel;
    GameObject pPanelName;
    Image imgPanel;
    int contadorPanel = 0;
    int contadorScreen = 0;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BtnInPopUp()
    {
        GOScroll.GetComponent<Image>().enabled = false;
        ControlScroll.SetActive(false);
        Screen.GetComponent<MeshRenderer>().enabled = false;
        pPanelName = GameObject.Find(EventSystem.current.currentSelectedGameObject.name);
        PanControl = GameObject.FindGameObjectsWithTag("TagTrag");
        foreach (GameObject i in PanControl)
        {
            contadorPanel++;
            i.GetComponent<Button>().interactable = false;
            i.GetComponent<Image>().enabled = false;
           

            if (pPanelName == i)
            {
                imgPanel = i.GetComponent<Image>();
                imgPanel.color = Color.HSVToRGB(24, 23, 23);
                btnHijoPanel = i.GetComponentsInChildren<Button>();
                foreach(Button b in btnHijoPanel)
                {                    
                    b.GetComponent<Image>().enabled = true;
                    b.GetComponent<Button>().interactable = true;
                    b.GetComponentInChildren<Text>().enabled = true;
                }
                i.GetComponent<Image>().enabled = true;
                PanelAnim = GameObject.FindGameObjectsWithTag("TagPanelAnim");
                foreach(GameObject j in PanelAnim)
                {
                    contadorScreen++;
                    if(contadorPanel == contadorScreen)
                    {
                        j.GetComponent<MeshRenderer>().enabled = true;                      
                        break;
                    }
                }
                
            }
        }
        contadorPanel = 0;
        contadorScreen = 0;
    }


    public void ButtonDesactivePopUp()
    {
        groupButton = GameObject.FindGameObjectsWithTag("BtnPanelPopUp");
        foreach (GameObject i in groupButton)
        {
            
            i.GetComponent<Button>().interactable = false;
            i.GetComponent<Image>().enabled = false;
            i.GetComponentInChildren<Text>().enabled = false;
        }

        PanControl = GameObject.FindGameObjectsWithTag("TagTrag");
        foreach (GameObject i in PanControl)
        {
            imgPanel = i.GetComponent<Image>();
            imgPanel.color = new Color(255, 255, 255);
            i.GetComponent<Button>().interactable = true;
            i.GetComponent<Image>().enabled = true;
        }

        PanControl = GameObject.FindGameObjectsWithTag("TagPanelAnim");
        foreach (GameObject i in PanControl)
        {
            i.GetComponent<MeshRenderer>().enabled = false;
        }
        GOScroll.GetComponent<Image>().enabled = true;
        Screen.GetComponent<MeshRenderer>().enabled = true;
        ControlScroll.SetActive(true);
    }
}
