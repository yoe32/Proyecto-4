using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cri : MonoBehaviour {

    public Button startButton;
    public bool playersReady;


    void Update()
    {
        // checks if the players are ready and if the start button is useable
        if (playersReady == true && startButton.interactable == false)
        {
            //allows the start button to be used
            startButton.interactable = true;
        }
    }
}
