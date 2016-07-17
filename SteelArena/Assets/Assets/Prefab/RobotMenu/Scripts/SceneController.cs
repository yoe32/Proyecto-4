using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public TorqueSpin obj;
    public ColliderButton leftArrow;
    public ColliderButton rightArrow;

    void OnEnable()
    {
        //leftArrow.OnClick += ArrowPressed;
        //rightArrow.OnClick += ArrowPressed;
    }

    void OnDisable()
    {
        //leftArrow.OnClick -= ArrowPressed;
        //rightArrow.OnClick -= ArrowPressed;
    }

    void ArrowPressed (ColliderButton pressedArrow)
    {
        switch (pressedArrow.name){
            case "LeftArrow":
                obj.SpinLeft();
                break;

            case "RightArrow":
                obj.SpinRight();
                break;
        }
    }

}
