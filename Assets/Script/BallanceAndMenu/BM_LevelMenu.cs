using UnityEngine;
using System.Collections;

public class BM_LevelMenu : ButtonsMenu
{
    public AudioSource DingAudio;

    public Animator LoadLevelAni;
    public new void OnEnable()
    {
        base.OnEnable();
        //检测已通关关卡
        ScoreSaver ss = new ScoreSaver(Application.persistentDataPath + "gamesave");
        for (int i = (ss.FirstLevel>1?ss.FirstLevel:1); i < 12; ++i)
        {
            if (Buttons == null)
                break;
            Buttons[i].SetDisable(true);
        }
    }

    public new void Start()
    {
        base.Start();
        //检测已通关关卡
        ScoreSaver ss = new ScoreSaver(Application.persistentDataPath + "gamesave");
        for (int i = (ss.FirstLevel > 1 ? ss.FirstLevel : 1); i < 12; ++i)
        {
            if (Buttons == null)
                break;
            Buttons[i].SetDisable(true);
        }
    }

    public override void OnButtinClicked(int ButtinId)
    {
        if (Buttons[ButtinId].Disable)
            return;
        base.OnButtinClicked(ButtinId);
        switch (ButtinId)
        {
            case 14:
                BackToParent();
                break;
            default:
                if (ButtinId >= 0 && ButtinId <= 12)
                {
                    //载入关卡
                    Debug.Log("Here load level " + (ButtinId + 1).ToString());
                    PlayerPrefs.SetString("LoadLevel", "Scene/Levels/Level" + (ButtinId + 1).ToString());
                    PlayerPrefs.SetInt("LoadLevelINT", ButtinId + 1);
                    DingAudio.Play();
                    DontDestroyOnLoad(DingAudio.gameObject);
                    LoadLevelAni.SetTrigger("LoadLevel");
                }
                break;
        }
    }
}
