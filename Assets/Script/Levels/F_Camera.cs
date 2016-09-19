using UnityEngine;
using System.Collections;

public class F_Camera : F_Object {
    private Vector3 CameraPosition;//摄像机位置最终

    private Vector3 CameraAimPosition;//观察目标最终位置

    public bool FreeCameraPosition = false, FreeCameraAim = false;//切换到自由摄像机模式
    public Vector3 FreeAim, FreePosition;//自由摄像机模式下摄像机的观察目标及位置


    private Vector3 CameraPositionA, CameraPositionB, CameraPositionC;
    private Vector3 CameraAimA, CameraAimB, CameraAimC;

    public float CameraAimS1, CameraAimS2, CameraAimS3;
    public float CameraPositionS1, CameraPositionS2, CameraPositionS3;

    private Transform theCamera;

    public Vector3 CameraMove;//硬性带方向
    public Vector3 CameraFirstSoftMove;//柔性带方向

    public Vector3 CameraSoftMove = new Vector3(0,0,0);//直接算到偏移中的，计算三级缓冲平滑处理

   
    public void SetFreePosition(bool isTrue)
    {
        FreeCameraPosition = isTrue;
    }
    public void SetFreePosition(bool isTrue , Vector3 pos)
    {
        FreeCameraPosition = isTrue;
        FreePosition = pos;
    }
    public void SetFreeAim(bool isTrue)
    {
        FreeCameraAim = isTrue;
    }
    public void SetFreeAim(bool isTrue, Vector3 pos)
    {
        FreeCameraAim = isTrue;
        FreeAim = pos;
    }
    public void SetDirection(Vector3 direction)
    {
        levelManager.CameraDirection.Rotate(direction);
    }
    
    // Use this for initialization
    new void Start () {
        base.Start();
        theCamera = GetComponent<Transform>();

        CameraAimPosition = levelManager.CameraAim == null ? levelManager.Player.GetComponent<Transform>().position : levelManager.CameraAim.position;
        CameraPosition = CameraAimPosition;//最后再加Direction和偏移量

        CameraAimA = CameraAimB = CameraAimC = CameraAimPosition;

        CameraPositionA = CameraPositionB = CameraPositionC = CameraPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (levelManager.isPause)
            return;
        CameraAimPosition = FreeCameraAim ? FreeAim : levelManager.CameraAim.position;

        CameraPosition = FreeCameraPosition ? FreePosition :  levelManager.CameraAim.position + levelManager.CameraDirection.TransformDirection(CameraSoftMove) + levelManager.CameraDirection.TransformDirection(CameraFirstSoftMove);//最后再加Direction和偏移量

        CameraAimA = (CameraAimPosition - CameraAimA) * CameraAimS1 + CameraAimA;
        CameraAimB = (CameraAimA - CameraAimB) * CameraAimS2 + CameraAimB;
        CameraAimC = (CameraAimB - CameraAimC) * CameraAimS3 + CameraAimC;//三级缓冲理论上可以不用，也就是说S3=1没问题
        theCamera.LookAt(CameraAimC);



        CameraPositionA = (CameraPosition - CameraPositionA) * CameraPositionS1 + CameraPositionA;
        CameraPositionB = (CameraPositionA - CameraPositionB) * CameraPositionS2 + CameraPositionB;
        CameraPositionC = (CameraPositionB - CameraPositionC) * CameraPositionS3 + CameraPositionC;//S3=1理论可以
        theCamera.position = CameraPositionC + (levelManager.CameraDirection.TransformDirection(CameraMove) /*- levelManager.CameraDirection.position*/);
    }
}
