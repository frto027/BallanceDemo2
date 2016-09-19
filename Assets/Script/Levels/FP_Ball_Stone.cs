﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 石球 玩家球
/// </summary>
public class FP_Ball_Stone : F_Player
{
    //private Vector3 position;

    public float GoForce = 10f;

    public GameObject BirthBall;

    private Rigidbody theRigidBody;

    private Vector3 memV, memR, memF;

    public AudioClip BallCrashAudioClip_Floor, BallCrashAudioClip_Wood, BallCrashAudioClip_Rail,
        BallScrollAudioClip_Floor, BallScrollAudioClip_Wood, BallScrollAudioClip_Rail;

    public float MaxVolumeSpeed = 3f;//相对速度达到后全音量播放滚动的音乐

    public GameObject StonePiece;
    public override void Start()
    {
        base.Start();
        //position = GetComponent<Transform>().position;
        theRigidBody = GetComponent<Rigidbody>();

        //初始化Audio
        if (levelManager.BallCrashAudio.isPlaying)
            levelManager.BallCrashAudio.Stop();
        //levelManager.BallCrashAudio.clip = BallCrashAudioClip;

        if (levelManager.BallScrollAudio_Floor.isPlaying)
            levelManager.BallScrollAudio_Floor.Stop();
        if (levelManager.BallScrollAudio_Wood.isPlaying)
            levelManager.BallScrollAudio_Wood.Stop();
        if (levelManager.BallScrollAudio_Rail.isPlaying)
            levelManager.BallScrollAudio_Rail.Stop();

        levelManager.BallScrollAudio_Floor.clip = BallScrollAudioClip_Floor;
        levelManager.BallScrollAudio_Wood.clip = BallScrollAudioClip_Wood;
        levelManager.BallScrollAudio_Rail.clip = BallScrollAudioClip_Rail;

        levelManager.BallScrollAudio_Floor.volume = 0;
        levelManager.BallScrollAudio_Wood.volume = 0;
        levelManager.BallScrollAudio_Rail.volume = 0;

        levelManager.BallScrollAudio_Floor.Play();
        levelManager.BallScrollAudio_Rail.Play();
        levelManager.BallScrollAudio_Wood.Play();

    }
    public override void Reset()
    {
        base.Reset();
        GameObject tempG = Instantiate(BirthBall);

        tempG.GetComponentInChildren<F_BirthBall>().GamePlayerToBeCreated = (Resources.Load("levels/FP_Ball_Stone_MF") as GameObject).GetComponent<FP_Ball_Stone>();
        tempG.GetComponent<Transform>().position = levelManager.RebirthPosition;// position;
        levelManager.Player = tempG.GetComponentInChildren<F_BirthBall>();
        tempG.GetComponentInChildren<F_BirthBall>().BirthBallAudio = GameObject.FindGameObjectWithTag("BirthBallAudio").GetComponent<AudioSource>();
        levelManager.CameraAim = levelManager.Player.GetComponent<Transform>();
        Destroy(gameObject);
    }
    public void OnCollisionEnter(Collision other)
    {
        //撞击音量
        AudioCaseMgr acm = other.gameObject.GetComponent<AudioCaseMgr>();
        if (acm == null)
            return;
        if (levelManager.BallCrashAudio.isPlaying)
            return;
        //设置音量
        //Rigidbody tOtherRigid = other.gameObject.GetComponent<Rigidbody>();
        float tempM = other.impulse.magnitude;//(theRigidBody.velocity - (tOtherRigid == null ? Vector3.zero : tOtherRigid.velocity)).magnitude;
        tempM = tempM > MaxVolumeSpeed ? 1 : tempM / MaxVolumeSpeed;
        //levelManager.BallCrashAudio.volume = tempM;
        if (acm.BallCrash_Floor)
            levelManager.BallCrashAudio.PlayOneShot(BallCrashAudioClip_Floor, tempM);
        else if (acm.BallCrash_Rail)
            levelManager.BallCrashAudio.PlayOneShot(BallCrashAudioClip_Rail, tempM);
        else if (acm.BallCrash_Wood)
            levelManager.BallCrashAudio.PlayOneShot(BallCrashAudioClip_Wood, tempM);
    }

    public void OnCollisionStay(Collision collision)
    {
        AudioCaseMgr acm = collision.gameObject.GetComponent<AudioCaseMgr>();
        if (acm == null)
            return;
        //计算音量
        Rigidbody tOtherRigid = collision.gameObject.GetComponent<Rigidbody>();
        float tempM = (theRigidBody.velocity - (tOtherRigid == null ? Vector3.zero : tOtherRigid.velocity)).magnitude;
        tempM = tempM > MaxVolumeSpeed ? 1 : tempM / MaxVolumeSpeed;
        if (acm.BallScroll_Floor)
            levelManager.BallScrollAudio_Floor.volume = tempM;
        if (acm.BallScroll_Wood)
            levelManager.BallScrollAudio_Wood.volume = tempM;
        if (acm.BallScroll_Rail)
            levelManager.BallScrollAudio_Rail.volume = tempM;
    }

    public void OnCollisionExit(Collision collision)
    {
        AudioCaseMgr acm = collision.gameObject.GetComponent<AudioCaseMgr>();
        if (acm == null)
            return;
        if (acm.BallScroll_Floor)
            levelManager.BallScrollAudio_Floor.volume = 0;
        if (acm.BallScroll_Wood)
            levelManager.BallScrollAudio_Wood.volume = 0;
        if (acm.BallScroll_Rail)
            levelManager.BallScrollAudio_Rail.volume = 0;
    }


    public override void Pause(bool isPause)
    {
        if (isPause)
        {
            memV = theRigidBody.velocity;
            memR = theRigidBody.angularVelocity;
            theRigidBody.isKinematic = true;
        }
        else
        {
            theRigidBody.velocity = memV;
            theRigidBody.angularVelocity = memR;
            theRigidBody.isKinematic = false;
        }
    }
    public void FixedUpdate()
    {
        if (levelManager.isPause || levelManager.DeathBigin)
            return;
        theRigidBody.AddForce(GetGoDirection() * GoForce);
    }
    public override void DestoryEffect()
    {
        Transform tf = GetComponent<Transform>();
        Instantiate(StonePiece, tf.position, tf.rotation);
    }
}
