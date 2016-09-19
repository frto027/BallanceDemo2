using UnityEngine;
using System.Collections;


/*
 * 手柄方向键监听类，一个场景请勿出现两个
 */
public class JoyListener : MonoBehaviour
{
    static public Vector2 L;
    static public Vector2 R;
    static public bool Up;
    static public bool Down;
    static public bool Left;
    static public bool Right;


    static public bool GetUpDown;
    static public bool GetDownDown;
    static public bool GetLeftDown;
    static public bool GetRightDown;

    static public bool HGetUpDown;
    static public bool HGetDownDown;
    static public bool HGetLeftDown;
    static public bool HGetRightDown;

    float FHUp, FHDown, FHLeft, FHRight;
    float HUp, HDown, HLeft, HRight;
    static public bool UUp;
    static public bool UDown;
    static public bool ULeft;
    static public bool URight;

    static public bool UGetUpDown;
    static public bool UGetDownDown;
    static public bool UGetLeftDown;
    static public bool UGetRightDown;

    static public bool UGetUpUp;
    static public bool UGetDownUp;
    static public bool UGetLeftUp;
    static public bool UGetRightUp;

    //旋转监听，非手柄事件
    static public bool LeftR;
    static public bool RightR;
    static public bool OverView;

    public float FirstHoldTime = 0.5f, BetweenHoldTime = 0.06f;


    // Use this for initialization
    public void Start()
    {
        LeftR = RightR = OverView = false;
        HGetUpDown = HGetDownDown = HGetLeftDown = HGetRightDown =
        UUp = UDown = ULeft = URight = UGetUpDown = UGetDownDown = UGetLeftDown = UGetRightDown =
        Up = Down = Left = Right = GetUpDown = GetDownDown = GetLeftDown = GetRightDown = false;

        FHUp = FHDown = FHLeft = FHRight = FirstHoldTime;
        HUp = HDown = HLeft = HRight = BetweenHoldTime;
    }

    public void FixedUpdate()
    {
        L = Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up;
        R = Input.GetAxis("RHorizontal") * Vector2.right + Input.GetAxis("RVertical") * Vector2.up;

        GetUpDown = (!Up) && Input.GetAxis("UpDown") > 0;
        GetDownDown = (!Down) && Input.GetAxis("UpDown") < 0;
        GetLeftDown = (!Left) && Input.GetAxis("LeftRight") < 0;
        GetRightDown = (!Right) && Input.GetAxis("LeftRight") > 0;
        Up = Input.GetAxis("UpDown") > 0;
        Down = Input.GetAxis("UpDown") < 0;
        Right = Input.GetAxis("LeftRight") > 0;
        Left = Input.GetAxis("LeftRight") < 0;

        HGetUpDown = false;
        if (!Up)
            FHUp = FirstHoldTime;
        else
        {
            if (FHUp > 0)
                FHUp -= Time.deltaTime;
            else
            {
                //这里第一次保持结束
                if (HUp > 0)
                {
                    HUp -= Time.deltaTime;
                }
                else
                {
                    HUp = BetweenHoldTime;
                    HGetUpDown = true;
                }
            }
        }

        HGetDownDown = false;
        if (!Down)
            FHDown = FirstHoldTime;
        else
        {
            if (FHDown > 0)
                FHDown -= Time.deltaTime;
            else
            {
                //这里第一次保持结束
                if (HDown > 0)
                {
                    HDown -= Time.deltaTime;
                }
                else
                {
                    HDown = BetweenHoldTime;
                    HGetDownDown = true;
                }
            }
        }
        HGetLeftDown = false;
        if (!Left)
            FHLeft = FirstHoldTime;
        else
        {
            if (FHLeft > 0)
                FHLeft -= Time.deltaTime;
            else
            {
                //这里第一次保持结束
                if (HLeft > 0)
                {
                    HLeft -= Time.deltaTime;
                }
                else
                {
                    HLeft = BetweenHoldTime;
                    HGetLeftDown = true;
                }
            }
        }
        HGetRightDown = false;
        if (!Right)
            FHRight = FirstHoldTime;
        else
        {
            if (FHRight > 0)
                FHRight -= Time.deltaTime;
            else
            {
                //这里第一次保持结束
                if (HRight > 0)
                {
                    HRight -= Time.deltaTime;
                }
                else
                {
                    HRight = BetweenHoldTime;
                    HGetRightDown = true;
                }
            }
        }//此处更新HGGetKeyDown用于记录保持按键间断长按获取

    }

    public void Update()
    {
        UGetUpUp = (UUp) && Input.GetAxis("UpDown") <= 0;
        UGetDownUp = (UDown) && Input.GetAxis("UpDown") >= 0;
        UGetLeftUp = (ULeft) && Input.GetAxis("LeftRight") >= 0;
        UGetRightUp = (URight) && Input.GetAxis("LeftRight") <= 0;

        UGetUpDown = (!UUp) && Input.GetAxis("UpDown") > 0;
        UGetDownDown = (!UDown) && Input.GetAxis("UpDown") < 0;
        UGetLeftDown = (!ULeft) && Input.GetAxis("LeftRight") < 0;
        UGetRightDown = (!URight) && Input.GetAxis("LeftRight") > 0;
        UUp = Input.GetAxis("UpDown") > 0;
        UDown = Input.GetAxis("UpDown") < 0;
        URight = Input.GetAxis("LeftRight") > 0;
        ULeft = Input.GetAxis("LeftRight") < 0;
    }
}
