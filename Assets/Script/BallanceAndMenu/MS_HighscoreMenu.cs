using UnityEngine;
using System.Collections;
using System.IO;

public class MS_HighscoreMenu : MenuScript
{
    public AudioSource ClickedAudio;
    public ButtonScript LeftButton;
    public ButtonScript RightButton;
    public UnityEngine.UI.Text[] NameTexts;//以此为准
    public UnityEngine.UI.Text[] ScoreTexts;
    public UnityEngine.UI.Text LevelText;
    private int NowLevel;// 0 to 12

    public void RefreshData()
    {
        //更新UI
        if (LevelText != null)
            LevelText.text = "Level " + (NowLevel + 1).ToString();
        //更新最高分数据

        //UI测试版本
        //for (int i = 0; i < NameTexts.Length; ++i)
        //{
        //    if (NameTexts[i] == null || ScoreTexts[i] == null)
        //        continue;
        //    NameTexts[i].text = "第" + (NowLevel + 1).ToString() + "关 第" + (i + 1).ToString() + "名";
        //    ScoreTexts[i].text = "L" + (NowLevel + 1).ToString() + "S" + (i + 1).ToString();
        //}

        //文件存取排行榜
        string gamesave = Application.persistentDataPath + "/gamesave";
        ScoreSaver ss = new ScoreSaver(gamesave);
        for(int i = 0; i < 10; ++i)
        {
            if (NameTexts[i] == null || ScoreTexts[i] == null)
                continue;
            NameTexts[i].text = ss.levelScoreSaver[NowLevel].name[i];
            ScoreTexts[i].text = ss.levelScoreSaver[NowLevel].score[i].ToString(); ;
        }
        ss.SaveToFile();
    }

    public void OnEnable()
    {
        NowLevel = 0;
        RefreshData();
    }

    public override void OnButtinClicked(int ButtinId)
    {
        if (ClickedAudio != null)
        {
            if (ClickedAudio.isPlaying)
                ClickedAudio.Stop();
            ClickedAudio.Play();
        }
        switch (ButtinId)
        {
            case 0://Left
                NowLevel = (NowLevel + 13 - 1) % 13;
                break;
            case 1://Right
                NowLevel = (NowLevel + 1) % 13;
                break;
            case 2://Back
                BackToParent();
                break;
        }
        RefreshData();
    }

    public void Update()
    {


        //按键检测
        if (Input.GetKeyDown(KeyCode.LeftArrow)||JoyListener.UGetLeftDown)
        {
            LeftButton.OnSelected(true);
            OnButtinClicked(0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)||JoyListener.UGetRightDown)
        {
            RightButton.OnSelected(true);
            OnButtinClicked(1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)||JoyListener.UGetLeftUp)
        {
            LeftButton.OnSelected(false);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)||JoyListener.UGetRightUp)
        {
            RightButton.OnSelected(false);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)||Input.GetButtonDown("B")|| Input.GetButtonDown("A"))
        {
            OnButtinClicked(2);
        }
    }
}
