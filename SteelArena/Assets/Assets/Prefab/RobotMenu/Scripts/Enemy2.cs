using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
    public GameObject GKey;
    public GameObject target;
    public bool isTrigger = false;
    // Use this for initialization

    void Start()
    {
        GKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {      
        if (isTrigger == true)
        {
            Line();
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

    void Line()
    {
        Vector3 PuntoA = gameObject.transform.position;
        Vector3 PuntoB = target.transform.position;
        Vector3 Longitud = PuntoB - PuntoA;
        float distancia = Vector3.Distance(PuntoA, PuntoB);
        Ray ray = new Ray(PuntoA, PuntoB);

        Debug.DrawRay(ray.origin, Longitud * 1.0f, Color.red);
        Debug.Log("Distancia: " + distancia);
    }

}
