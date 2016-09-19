using UnityEngine;
using System.Collections;

public class P_Modul_29_Script : MonoBehaviour
{
    public HingeJoint StoneBrakHingeJoint;

    public void OnCollisionEnter(Collision collision)
    {
        if (StoneBrakHingeJoint == null)//已经破坏
            return;
        F_Player FP = collision.gameObject.GetComponent<F_Player>();
        if (F_Object.levelManager.Player != FP)
            return;
        if (FP.GetComponent<FP_Ball_Stone>() != null)
        {
            StoneBrakHingeJoint.breakForce = 10;
        }
        else
        {
            StoneBrakHingeJoint.breakForce = float.PositiveInfinity;
        }
    }
}
