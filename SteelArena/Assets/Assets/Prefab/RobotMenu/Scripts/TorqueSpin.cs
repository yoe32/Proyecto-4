using UnityEngine;
using System.Collections;

public class TorqueSpin : MonoBehaviour {

    public float spinForce = 300f;
    Rigidbody cachedRigidBody;

    void Awake()
    {
        
    }

    public void SpinLeft()
    {
        cachedRigidBody.AddTorque(Vector3.up * spinForce);
    }

    public void SpinRight()
    {
        cachedRigidBody.AddTorque(Vector3.up * -spinForce);
    }
}
