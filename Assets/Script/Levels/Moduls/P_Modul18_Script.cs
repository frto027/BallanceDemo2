using UnityEngine;
using System.Collections;
/// <summary>
/// 纸球进入trigger，受到向上的力
/// </summary>
public class P_Modul18_Script : MonoBehaviour
{
    public Vector3 force;
    public void OnTriggerStay(Collider other)
    {
        F_Player oplayer = other.GetComponent<F_Player>();
        if (oplayer as FP_Ball_Paper != null && oplayer == F_Object.levelManager.Player)
            oplayer.GetComponent<Rigidbody>().AddForce(force);
    }
}
