using UnityEngine;
using System.Collections;

public class SerialUISummoner : MonoBehaviour {

    public GameObject GPanelM;
    public GameObject GPanelS;
    public GameObject GPanelW;
    public GameObject objetEnd;
    public bool showing = false;
    public bool isTrigger = false;
    public float delay = 0.1f; 
    protected Animator[] children;

	// Use this for initialization
	void Start () {
        GPanelM.SetActive(false);
        GPanelS.SetActive(false);
        GPanelW.SetActive(false);
        children = GetComponentsInChildren<Animator>();
        //for (int a = 0; a < children.Length; a++)
        //{
        //    children[a].SetBool("Shown", showing);
        //}
    }

    // Update is called once per frame
    void Update () {

        if (isTrigger == true)
        {
            GPanelM.SetActive(true);
            //GKe.SetActive(false);
            if (showing) return;
            StartCoroutine("ActivateInTurn");
        }
        else
        {

            if (!showing) return;
            StartCoroutine("DeactivateInTurn");
        }

    }

    public IEnumerator ActivateInTurn()
    {
        showing = true;
        yield return new WaitForSeconds(delay);
        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", true);
            yield return new WaitForSeconds(delay);
        }
    }

    public IEnumerator DeactivateInTurn()
    {
        showing = false;
        yield return new WaitForSeconds(delay);
        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", false);
            yield return new WaitForSeconds(delay);
        }
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
