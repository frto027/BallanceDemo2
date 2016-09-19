using UnityEngine;
using System.Collections;
/*
 * 死亡区域，最好无刚体，玩家进入触发死亡
 * */
public class Area_Death : MonoBehaviour
{
    LevelManager levelManager;

    public void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() == levelManager.Player)
            levelManager.Death();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<F_Player>() == levelManager.Player)
            levelManager.Death();
    }
}
