using UnityEngine;
using System.Collections;

public class WhiteBlackAnim : MonoBehaviour
{
    private LevelManager levelManager;
    public AudioSource RebirthAudio;

    public void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public void RunDeathReset()
    {
        levelManager.DeathReset();
    }
    public void PlayRebirthAudio()
    {
        if (RebirthAudio.isPlaying)
            RebirthAudio.Stop();
        RebirthAudio.Play();
    }
    public void RunDeathEnd()
    {
        levelManager.OverDeath();
    }
}
