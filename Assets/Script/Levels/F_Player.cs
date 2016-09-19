using UnityEngine;
using System.Collections;

public class F_Player : F_Object {
    public enum PlayerTypes { Paper,Wood,Stone};
    public PlayerTypes PlayerType;
    public Vector3 GetGoDirection()//根据JoyListener.L返回一个前进的方向向量，注意横纵坐标独立，最大长度为根号2而非1，即非90增益
    {
        if (levelManager.Player != this || levelManager.changingBall)
            return Vector3.zero;
        Vector3 v3 = Vector3.zero;
        v3.x = JoyListener.L.x;
        v3.z = JoyListener.L.y;
        return levelManager.CameraDirection.TransformDirection(v3);
    }
    public virtual void DestoryEffect()
    {

    }
}
