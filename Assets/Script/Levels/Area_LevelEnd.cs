using UnityEngine;
using System.Collections;
/*
 * 轻触通关
 * */
public class Area_LevelEnd : MonoBehaviour
{
    LevelManager levelManager;
    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() == levelManager.Player)
            levelManager.LevelEnd();
    }
}
