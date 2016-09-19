using UnityEngine;
using System.Collections;

public class BallanceAnims : MonoBehaviour
{
    public GameObject MainMenu;//动画播放结束后想要显示的第一个场景
    public AudioSource BGMAudio;
    public AudioClip BGMClip;
    bool BGMing;

    public void OnEnable()
    {
        BGMing = false;
        GetComponent<Animator>().SetBool("Skip", PlayerPrefs.GetInt("PlayBallance", 1) == 0);
        GetComponent<Animator>().SetTrigger("Play");
    }

    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
    }
    public void PlayBallance()
    {
        BGMing = false;
        BGMAudio.Play();
    }
    public void PlayBGM()
    {
        BGMing = true;
    }

    public void Update()
    {
        if (BGMing && !BGMAudio.isPlaying)
        {
            BGMAudio.clip = BGMClip;
            BGMAudio.loop = true;
            BGMAudio.Play();
            Destroy(this);
        }
    }
}
