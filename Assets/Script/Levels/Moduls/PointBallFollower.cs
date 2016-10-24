using UnityEngine;
using System.Collections;
/// <summary>
/// 分数球旋转 跟踪 碰撞 加分
/// （会围绕centerObject对象位置旋转）
/// </summary>
public class PointBallFollower : F_Object
{
    public Transform centerObject;
    public Vector3 direction;
    public float angleSpeed, speed;
    private Transform theTrans;

    public int FollowBall = 0;//0 旋转 1 飞溅 2 跟随 3 完成
    public int ScoreToAdd = 200;

    public float outDistance;//飞溅距离
    public float outDistanceMin;//飞溅阈值，比0稍大即可，不宜为0，不宜为负
    public float outDistanceK;

    public float followBallK;
    public ParticleSystem crashBallPaiticle;//碰球后加分特效


    private Vector3 aimPosition;//true aim position
    private Vector3 aimPosition2;//buffer1
    public float aimP1To2;
    public float aimP2To3;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        theTrans = GetComponent<Transform>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (FollowBall == 2 && other.GetComponent<F_Player>() == levelManager.Player)
        {
            levelManager.Score += 200;
            //销毁动画
            GetComponent<Collider>().enabled = false;//disable colider
            GetComponent<ParticleSystem>().maxParticles = 0;//disable ball
            if (crashBallPaiticle != null)
            {
                Transform cbTrans = crashBallPaiticle.GetComponent<Transform>();
                cbTrans.LookAt(levelManager.Player.GetComponent<Transform>());
                cbTrans.Translate(Vector3.forward * GetComponent<SphereCollider>().radius);
                crashBallPaiticle.Play();
            }
            FollowBall = 3;
            Destroy(this);
        }
    }

    void FixedUpdate()
    {
        switch (FollowBall)
        {
            case 0:
                theTrans.RotateAround(centerObject.position, direction, angleSpeed * Time.fixedDeltaTime);
                aimPosition2 = aimPosition = aimPosition = theTrans.position;
                break;
            case 1:
                {
                    float nowDistance = (aimPosition - centerObject.position).magnitude;
                    float moreDistance = outDistance - nowDistance;
                    moreDistance = outDistanceK * moreDistance;
                    nowDistance = outDistance - moreDistance;
                    aimPosition = centerObject.position + (aimPosition - centerObject.position).normalized * nowDistance;
                    if (/*moreDistance*/ outDistance - (aimPosition2 - centerObject.position).magnitude <= outDistanceMin)
                    {
                        FollowBall = 2;
                        //play some audio
                    }
                }
                goto case -1;//async position
            case 2:
                {
                    Vector3 playerPosition = levelManager.Player.GetComponent<Transform>().position;
                    float nowDistance = (aimPosition - playerPosition).magnitude;
                    nowDistance = nowDistance * followBallK;
                    aimPosition = playerPosition + (aimPosition - playerPosition).normalized * nowDistance;
                }
                goto case -1;
            case -1:
                {
                    aimPosition2 = aimPosition2 + (aimPosition - aimPosition2) * aimP1To2;
                    theTrans.position = theTrans.position + (aimPosition2 - theTrans.position) * aimP2To3;
                }
                break;
        }
    }

    public void BallCome()
    {
        FollowBall = 1;
    }

}
