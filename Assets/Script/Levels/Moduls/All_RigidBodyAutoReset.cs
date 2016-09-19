using UnityEngine;
using System.Collections;

public class All_RigidBodyAutoReset : F_Object
{
    public string resourceDir;
    private Vector3 initPos;
    private Quaternion initRot;
    public new void Start()
    {
        base.Start();
        initPos = GetComponent<Transform>().position;
        initRot = GetComponent<Transform>().rotation;
    }
    public override void Reset()
    {
        Instantiate(Resources.Load(resourceDir) as GameObject, initPos, initRot);
        Destroy(gameObject);
    }
}
