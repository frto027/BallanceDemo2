using UnityEngine;
using System.Collections;
/*让物体与玩家坐标一致，有碰撞者慎用*/
public class FollowScirpt : F_Player {

    private Transform theT;

    public override void Start()
    {
        base.Start();
        theT = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update () {
        theT.position = levelManager.Player.GetComponent<Transform>().position;
	}
}
