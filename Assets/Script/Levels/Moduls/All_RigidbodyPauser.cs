using UnityEngine;
using System.Collections;

/// <summary>
/// 接收暂停相应时间，让刚体暂停
/// </summary>

public class All_RigidbodyPauser : F_Object
{
    Rigidbody theRigidbody;
    bool Sleepy;

    Vector3 V, AV;
    public override void Start()
    {
        base.Start();
        theRigidbody = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        Sleepy = false;
    }

    public override void Pause(bool isPause)
    {
        if (theRigidbody == null)
        {
            Debug.Log(name + " has no Rigidbody");
            return;
        }
        if (isPause)
        {
            Sleepy = true;
            V = theRigidbody.velocity;
            AV = theRigidbody.angularVelocity;
            theRigidbody.Sleep();
        }
        else
        {
            Sleepy = false;
            theRigidbody.WakeUp();
            theRigidbody.velocity = V;
            theRigidbody.angularVelocity = AV;
        }
    }

    public void Update()
    {
        if (Sleepy)
        {
            theRigidbody.Sleep();
        }
    }
}
