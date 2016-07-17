using UnityEngine;
using System.Collections;

public class ButtPanelAction : MonoBehaviour {

    public GameObject Obj;

   public void On_Click()
    {
        Instantiate(Obj, transform.position, transform.rotation);
    }
}
