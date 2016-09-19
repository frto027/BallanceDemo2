using UnityEngine;
using System.Collections;

public class F_BirthBall : F_Player
{
    public AudioSource BirthBallAudio;

    private Renderer theRenderer;
    private float time, alltime;
    public Texture [] textures;
    private int now;
    public float SwitchSpeed = 0.2f;

    public ParticleSystem[] ParticleSystems;

    public F_Player GamePlayerToBeCreated;


    public override void Start()
    {
        base.Start();
        levelManager.birthing = true;
        theRenderer = GetComponent<Renderer>();
        time = 0;alltime = 0;
        now = 0;
        foreach(ParticleSystem ps in ParticleSystems)
        {
            ps.loop = true;
        }
    }

    public void FixedUpdate()
    {
        if (!levelManager.DeathBigin && !levelManager.isPause)
            GetComponent<Animator>().SetTrigger("Play");
        time += Time.deltaTime;
        if(time >= SwitchSpeed)
        {
            time = 0;
            now = (now + 1) % textures.Length;
            theRenderer.material.SetTexture("_MainTex", textures[now]); 
        }
    }
    public void CreatSmoke()
    {
        foreach(ParticleSystem ps in ParticleSystems)
        {
            ps.Play();
            ps.loop = false;
        }
    }
    public void CreatGamePlayer()
    {
        //Debug.Log("Creat!");
        levelManager.Player = Instantiate<F_Player>(GamePlayerToBeCreated);
        levelManager.CameraAim = levelManager.Player.GetComponent<Transform>();
        levelManager.RebirthPosition = levelManager.CameraAim.position = GetComponent<Transform>().position;
        gameObject.SetActive(false);
        levelManager.birthing = false;
    }
    public override void Reset()
    {
        base.Reset();
        Destroy(gameObject);
    }
    public void PlayAudio()
    {
        if (BirthBallAudio.isPlaying)
            BirthBallAudio.Stop();
        BirthBallAudio.Play();
    }
}
