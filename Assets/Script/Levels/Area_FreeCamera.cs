using UnityEngine;
using System.Collections;
/*
 * 进入此Area后触发Tragger，将摄像机切换为自由模式，给一个自由视角
 * */
public class Area_FreeCamera : F_Object
{
    public bool DoIn = false;
    public bool DoOut = false;


    public bool FreeAim;
    public bool FreePosition;
    public Transform AimObj;
    public Vector3 Aim;//可选择某物或者某座标为观察目标
    public Transform positionObj;
    public Vector3 position;
    public int CameraDirection;//这是玩家按前进键前进的方向0 1 2 3对应URDL
    public int OutDirection;// 0 1 2 3对应URDL

    private Animator CameraDirectionAnim;

    private bool isIn;

    public new void Start()
    {
        base.Start();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        CameraDirectionAnim = levelManager.CameraDirection.GetComponent<Animator>();
        isIn = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (DoIn == false)
            return;
        if (other.GetComponent<F_Player>() != levelManager.Player)
            return;
        isIn = true;
        if (FreePosition)
            levelManager.theCamera.SetFreePosition(true, positionObj == null ? position : positionObj.position);
        if (FreeAim)
            levelManager.theCamera.SetFreeAim(true, AimObj == null ? Aim : AimObj.position);
        string aimTrigger = "DU";
        switch (CameraDirection)
        {
            case 0:
                aimTrigger = "DU";
                break;
            case 1:
                aimTrigger = "DR";
                break;
            case 2:
                aimTrigger = "DD";
                break;
            case 3:
                aimTrigger = "DL";
                break;
        }


        CameraDirectionAnim.ResetTrigger("U");
        CameraDirectionAnim.ResetTrigger("R");
        CameraDirectionAnim.ResetTrigger("D");
        CameraDirectionAnim.ResetTrigger("L");
        CameraDirectionAnim.SetTrigger(aimTrigger);
    }

    public void Update()
    {
        if (DoIn == false || isIn == false)
            return;

        if (FreePosition)
            levelManager.theCamera.SetFreePosition(true, positionObj == null ? position : positionObj.position);
        if (FreeAim)
            levelManager.theCamera.SetFreeAim(true, AimObj == null ? Aim : AimObj.position);
    }

    public void OnTriggerExit(Collider other)
    {
        isIn = false;
        if (DoOut == false)
            return;
        if (other.GetComponent<F_Player>() != levelManager.Player)
            return;
        levelManager.theCamera.SetFreeAim(false);
        levelManager.theCamera.SetFreePosition(false);
        string aimTrigger = "U";
        switch (OutDirection)
        {
            case 0:
                aimTrigger = "U";
                break;
            case 1:
                aimTrigger = "R";
                break;
            case 2:
                aimTrigger = "D";
                break;
            case 3:
                aimTrigger = "L";
                break;
        }
        CameraDirectionAnim.SetTrigger(aimTrigger);
    }
    public override void Reset()
    {
        base.Reset();
        if (isIn)
        {
            levelManager.theCamera.SetFreeAim(false);
            levelManager.theCamera.SetFreePosition(false);
            string aimTrigger = "U";
            switch (OutDirection)
            {
                case 0:
                    aimTrigger = "U";
                    break;
                case 1:
                    aimTrigger = "R";
                    break;
                case 2:
                    aimTrigger = "D";
                    break;
                case 3:
                    aimTrigger = "L";
                    break;
            }
            CameraDirectionAnim.SetTrigger(aimTrigger);
        }
    }
}
