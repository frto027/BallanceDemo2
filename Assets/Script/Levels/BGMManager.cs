using UnityEngine;
using System.Collections;
/// <summary>
/// 管理背景音乐资源，被禁用后BGM消失
/// </summary>
public class BGMManager : F_Object
{
    public AudioClip[] Bgms;
    public float[] playSeconds;//playSeconds[i]为第i+1小节音乐首次播放延迟，单位秒，不存在则立即播放
    public float[] loopSeconds;//loopSeconds[i]为第i+1小节音乐两次播放开启间隔【注意包含音乐本身时长】，不存在或小于0则默认开启循环

    private float loopCount;

    public override void Start()
    {
        base.Start();
        SwitchSector();
    }

    public override void SwitchSector()
    {
        if (levelManager.BackGroundMusicAudio.isPlaying)
            levelManager.BackGroundMusicAudio.Stop();
        levelManager.BackGroundMusicAudio.loop = false;
        if (playSeconds == null || playSeconds.Length < levelManager.CurrentSector)
            loopCount = 0f;
        else
            loopCount = playSeconds[levelManager.CurrentSector - 1];
    }

    public void FixedUpdate()
    {
        if (levelManager.BackGroundMusicAudio.loop)
            return;
        loopCount -= Time.fixedDeltaTime;
        if (loopCount <= 0)
        {
            PlayMusic();
            if(loopSeconds == null || loopSeconds.Length < levelManager.CurrentSector || loopSeconds[levelManager.CurrentSector-1] <=0)
            {
                levelManager.BackGroundMusicAudio.loop = true;
            }else
            {
                loopCount = loopSeconds[levelManager.CurrentSector -1];
            }
        }
    }
    private void PlayMusic()
    {
        if(Bgms != null && Bgms.Length >= levelManager.CurrentSector)
        {
            if (levelManager.BackGroundMusicAudio.isPlaying)
                levelManager.BackGroundMusicAudio.Stop();
            levelManager.BackGroundMusicAudio.clip = Bgms[levelManager.CurrentSector - 1];
            levelManager.BackGroundMusicAudio.Play();
        }
    }
}
