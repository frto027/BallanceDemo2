using UnityEngine;
using System.Collections;

public class DestoryByTime : MonoBehaviour
{
    public float TimeToDestory = 5f;

    public void FixedUpdate()
    {
        TimeToDestory -= Time.deltaTime;
        if (TimeToDestory <= 0)
            Destroy(gameObject);
    }
}
