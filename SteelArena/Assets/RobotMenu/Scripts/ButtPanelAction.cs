using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtPanelAction : MonoBehaviour
{

    public GameObject ObjPrefabs;
    public GameObject ObjPos;
    private GameObject ObjHijo;
    private GameObject objMecha;
    public GameObject posMecha;
    public GameObject Esfera;
    GameObject[] Top;
    GameObject[] pPanControl;

    public void objState()
    {
        ObjHijo = (GameObject)Instantiate(ObjPrefabs.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        ObjHijo.transform.position = ObjPos.transform.position;
        ObjHijo.transform.localRotation = ObjPos.transform.rotation;
        ObjHijo.transform.localScale = ObjPos.transform.localScale;
        ObjHijo.transform.parent = ObjPos.transform;
    }

    public void objMechaState()
    {
        objMecha = (GameObject)Instantiate(ObjPrefabs.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        objMecha.transform.position = posMecha.transform.position;
        objMecha.transform.parent = posMecha.transform;
    }

    public void pointPrefabs()
    {
        pPanControl = GameObject.FindGameObjectsWithTag("TagPointPrefabs");
        foreach (GameObject i in pPanControl)
        {
         //   GameObject pEsfera = Instantiate(Esfera.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
         //   pEsfera.transform.parent = i.transform;
         //   pEsfera.transform.position = i.transform.position;
        }
    }

    public void pointPrefabs2()
    {
        pPanControl = GameObject.FindGameObjectsWithTag("TagPointPrefabs");
        foreach (GameObject i in pPanControl)
        {
            GameObject pEsfera = Instantiate(ObjPrefabs.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            pEsfera.transform.parent = i.transform;
            pEsfera.transform.position = i.transform.position;
        }
    }

    public void pointPrefabsEsfera()
    {
        Top = GameObject.FindGameObjectsWithTag("PointTop");
        foreach (GameObject i in Top)
        {
            GameObject pEsferaTop = Instantiate(Esfera.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            pEsferaTop.transform.parent = i.transform;
            pEsferaTop.transform.position = i.transform.position;
        }
        

        GameObject[] Back = GameObject.FindGameObjectsWithTag("PointBack");
        foreach (GameObject i in Back)
        {
            GameObject pEsferaBack = Instantiate(Esfera.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            pEsferaBack.transform.parent = i.transform;
            pEsferaBack.transform.position = i.transform.position;
        }

        GameObject[] Left = GameObject.FindGameObjectsWithTag("PointLeft");
        foreach (GameObject i in Left)
        {
            GameObject pEsferaLeft = Instantiate(Esfera.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            pEsferaLeft.transform.parent = i.transform;
            pEsferaLeft.transform.position = i.transform.position;
        }

        GameObject[] Right = GameObject.FindGameObjectsWithTag("PointRight");
        foreach (GameObject i in Right)
        {
            GameObject pEsferaRight = Instantiate(Esfera.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            pEsferaRight.transform.parent = i.transform;
            pEsferaRight.transform.position = i.transform.position;
        }
    }

   

    public void btnDestroyPrefabs()
    {
        Destroy(ObjHijo, 0.01f);
    }

   void Start()
    {

    }
}
