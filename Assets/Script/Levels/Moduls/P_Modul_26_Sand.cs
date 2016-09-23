using UnityEngine;
using System.Collections;

public class P_Modul_26_Sand : F_Object
{
    public float Height;
    public float BottomSpeed;
    public float MaxForce;
    Transform theTrans;
    Rigidbody theRigid;

    public new void Start()
    {
        base.Start();
        theTrans = GetComponent<Transform>();
        theRigid = GetComponent<Rigidbody>();

    }

    public void FixedUpdate()
    {
        if (!levelManager.isPause &&theTrans.localPosition.z<Height)
        {
            if (theTrans.localPosition.y*theTrans.InverseTransformDirection(theRigid.velocity).y >=0)
            {
                float AimSpeed = Mathf.Sqrt(BottomSpeed * BottomSpeed - 2 * Mathf.Abs(Physics.gravity.z) * theTrans.localPosition.z);
                float AimForce = (AimSpeed - Mathf.Abs(theTrans.InverseTransformDirection(theRigid.velocity).y)) * MaxForce;
                theRigid.AddForce(theTrans.TransformDirection(new Vector3(0, AimForce * (theTrans.localPosition.y >= 0 ? 1 : -1))));
            }
        }
    }
}

