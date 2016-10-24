using UnityEngine;
using System.Collections;

public class TestAutoMoveFoward : MonoBehaviour
{
    public float speed;
    private Transform trams;
    // Use this for initialization
    void Start()
    {
        trams = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        trams.position = trams.position + Vector3.forward * speed * Time.fixedDeltaTime;
    }
}
