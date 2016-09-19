using UnityEngine;
using System.Collections;
/// <summary>
/// 控制速度脚本，让飞船的速度稳定在某个值
/// </summary>
public class PE_Balloon_VelocitySteady : MonoBehaviour
{
    public float k = 1f;
    public bool positionMode = true;
    public float positionK = 1f;

    private Transform theTrans;
    private Vector3 initPos;

    public Vector3 AimVelocity = Vector3.zero;
    public Vector3 MaxForce = Vector3.zero;
    //ConstantForce T_constantForce;
    Rigidbody rigidBody;

    public void Start()
    {
        //T_constantForce = GetComponent<ConstantForce>();
        rigidBody = GetComponent<Rigidbody>();
        theTrans = GetComponent<Transform>();
        initPos = theTrans.position;
    }

    public void FixedUpdate()
    {
        if (positionMode)
        {
            Vector3 temp = Vector3.zero;
            temp.y = (initPos.y - theTrans.position.y) * positionK * Time.deltaTime;
            rigidBody.AddRelativeForce(temp * Time.deltaTime);
            return;
        }
        Vector3 force = Vector3.zero;//T_constantForce.force;
        force.x = k * (AimVelocity.x - rigidBody.velocity.x);
        force.x = force.x > MaxForce.x ? MaxForce.x : force.x;
        force.x = force.x < -MaxForce.x ? -MaxForce.x : force.x;


        force.y = k * (AimVelocity.y - rigidBody.velocity.y);
        force.y = force.y > MaxForce.y ? MaxForce.y : force.y;
        force.y = force.y < -MaxForce.y ? -MaxForce.y : force.y;

        force.z = k * (AimVelocity.z - rigidBody.velocity.z);
        force.z = force.z > MaxForce.z ? MaxForce.z : force.z;
        force.z = force.z < -MaxForce.z ? -MaxForce.z : force.z;

        //Debug.Log(force);
        // T_constantForce.force = force;
        rigidBody.AddRelativeForce(force * Time.deltaTime);
    }
}
