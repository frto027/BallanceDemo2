using UnityEngine;
using System.Collections;

public class FlappyPaperBall : F_Object
{
    public void OnTriggerEnter(Collider other)
    {
        if(levelManager.Player == other.GetComponent<F_Player>() && levelManager.Player.PlayerType == F_Player.PlayerTypes.Paper)
        {
            levelManager.Player.GetComponent<Transform>().localScale = new Vector3(0.1f, 1, 1);
        }
    }
}
