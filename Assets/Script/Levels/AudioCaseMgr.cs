using UnityEngine;
using System.Collections;
/// <summary>
/// 声音管理，玩家球在行走的时候处理碰撞，根据被碰撞物体是否由此脚本来判断发声
/// </summary>
public class AudioCaseMgr : MonoBehaviour {
    //击中
    public bool BallCrash_Floor = false;//路面
    public bool BallCrash_Wood = false;//木板
    public bool BallCrash_Rail = false;//钢轨
    //走
    public bool BallScroll_Floor = false;
    public bool BallScroll_Wood = false;
    public bool BallScroll_Rail = false;
}
