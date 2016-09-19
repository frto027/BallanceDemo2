using UnityEngine;
using System.Collections;
/// <summary>
/// 自动删除变球器碎片（变透明）
/// </summary>
public class BallTrafoPieceAutoDistory : F_Object
{
    int state;// deBirthed, birthed, disting;
    float disNowTime;
    public float DisableTime;
    public Material BallPieceMaterial;
    public float AutoDestoryTime;

    public void OnEnable()
    {
        state = 0;
        
    }

    public void FixedUpdate()
    {
        if (state <= 1)
        {
            AutoDestoryTime -= Time.deltaTime;
            if (AutoDestoryTime <= 0)
            {
                state = 2;//Time Up
                disNowTime = DisableTime;
            }
        }
        switch (state)
        {
            case 0:
                //本次change未结束
                if (!levelManager.changingBall)
                    state = 1;//change over
                break;
            case 1:
                if (levelManager.changingBall)
                {
                    state = 2;//下次change已开始，开始销毁
                    disNowTime = DisableTime;
                }
                break;
            case 2:
                disNowTime -= Time.deltaTime;
                if (disNowTime < 0)
                {
                    state = 3;
                    goto case 3;
                }
                Color color = BallPieceMaterial.color;
                color.a = disNowTime / DisableTime;
                BallPieceMaterial.color = color;
                break;
            case 3://换球结束
                Destroy(gameObject);
                Color colors = BallPieceMaterial.color;
                colors.a = 1f;
                BallPieceMaterial.color = colors;
                state = 4;
                break;
            default:
                break;
        }
    }
}
