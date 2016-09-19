using UnityEngine;
using System.Collections;

public class TrafoPieceBoomFirst : MonoBehaviour
{
    public float strength;
    public Vector3 direction;

    public void FixedUpdate()
    {
        direction.Normalize();
        GetComponent<Rigidbody>().velocity = GetComponent<Transform>().TransformDirection(direction) * strength;
        Destroy(this);
    }
}
