using UnityEngine;
using System.Collections;

public class P_Modul_03_Script : F_Object
{
    Vector3 initPos;
    Rigidbody theRigid;
    Transform theTrans;
    public float initMessForce;
    public float k;
    public new void Start()
    {
        base.Start();
        theRigid = GetComponent<Rigidbody>();
        theTrans = GetComponent<Transform>();
        initPos = theTrans.position;
    }

    public void FixedUpdate()
    {
        if (!levelManager.isPause)
        {
            theRigid.AddForce((initPos - theTrans.position ) * k -+ initMessForce * Physics.gravity);
        }
    }
}
