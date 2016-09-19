using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MS_HighscoreInputUI : MenuScript
{
    public float theScore;
    public GameObject GameEndMenu;
    public GameObject HighscoreMenu;
    public Text ScoreText;
    public Text PlayerNameText;

    public AudioSource EnableAudio;

    public void OnEnable()
    {
        ScoreText.text = ((int)theScore) + " Points";
        if (EnableAudio.isPlaying)
            EnableAudio.Stop();
        EnableAudio.Play();
        
    }

    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);
        //一定是返回按钮了
        ScoreSaver ss = new ScoreSaver(Application.persistentDataPath + "/gamesave");
        ss.levelScoreSaver[GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().CurrentLevel].AddScore(PlayerNameText.text == ""?"Name_": PlayerNameText.text, (int)theScore);
        ss.SaveToFile();
        GoToMenu(HighscoreMenu);
        //这里重定义父窗口
        HighscoreMenu.GetComponent<MenuScript>().ParentMenu = GameEndMenu;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnButtinClicked(0);
    }
}
