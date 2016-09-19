using UnityEngine;
using System.Collections;
/*
 * 一个F_Player
 * 直接根据前进方向改变坐标
 * 非90度增速
 */
public class FP_DirectMove : F_Player
{
    private Transform theTrans;

    public new void Start()
    {
        base.Start();
        theTrans = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        if (levelManager.isPause)
            return;
        theTrans.position = theTrans.position + GetGoDirection() * Time.deltaTime * 5;
    }
}
