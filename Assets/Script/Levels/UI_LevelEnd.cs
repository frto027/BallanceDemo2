using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_LevelEnd : MenuScript
{
    private LevelManager levelManager;
    private int state;
    private float delays;
    private float LB, PS, EL, ST;
    public Text LevelBonus, PointScore, ExtraLife, ScoreText;

    public RectTransform LightTrans;
    public Vector2 pos1, pos2, pos3, pos4;


    public GameObject LevelEndMenu;
    public MS_HighscoreInputUI HighScoreInputUI;

    public AudioSource EnableAudio;
    public void OnEnable()
    {
        state = -1;
        delays = 2;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        if (EnableAudio.isPlaying)
            EnableAudio.Stop();
        EnableAudio.Play();
    }

    public void FixedUpdate()
    {
        //自动机

        switch (state)
        {
            case -1://播放音乐，等待2秒
                if (delays > 0)
                    delays -= Time.deltaTime;
                else
                {
                    delays = 1;
                    state = 0;
                    GetComponent<Canvas>().enabled = true;
                }
                break;
            case 0:
                //分数1，LevelBonus
                if (delays == 1)
                    LB = 100;
                LevelBonus.text = ((int)LB).ToString();
                LightTrans.anchoredPosition = pos1;
                delays -= Time.deltaTime;
                if(delays <= 0)
                {
                    state = 1;
                    delays = 1;
                }
                PS = 0;
                break;
            case 1:
                LightTrans.anchoredPosition = pos2;
                if(levelManager.Score > 0)
                {
                    float delta = (levelManager.Score > 500 * Time.deltaTime  ? 500 * Time.deltaTime: levelManager.Score);
                    if (Input.GetKeyDown(KeyCode.Return))
                        delta = levelManager.Score;
                    levelManager.Score -= delta;
                    PS += delta;
                    PointScore.text = ((int)PS).ToString();
                }else
                {
                    delays -= Time.deltaTime;
                    if(delays <= 0)
                    {
                        //next
                        state = 2;
                        delays = 1;
                        EL = 0;
                    }
                }
                //Score
                break;
            case 2:
                //extralives
                LightTrans.anchoredPosition = pos3;
                if(delays > 0)
                {
                    delays -= Time.deltaTime;
                }
                else
                {
                    delays = 1;
                    if(levelManager.ExtraLife > 0)
                    {
                        levelManager.ExtraLife--;
                        EL += 200;
                        ExtraLife.text = ((int)EL).ToString();
                    }else
                    {
                        state = 3;
                        ST = 0;
                        delays = 1;
                    }
                }

                break;
            case 3:
                //score
                LightTrans.anchoredPosition = pos4;
                if (delays > 0)
                    delays -= Time.deltaTime;
                else
                {
                    ST = PS + EL + LB;
                    ScoreText.text = ((int)ST).ToString();
                    delays = 1;
                    state = 5;
                }
                break;
            case 5:
                //showmenu
                if (delays > 0)
                    delays -= Time.deltaTime;
                else
                {
                    //showmenu
                    state = 6;//不存在
                    if(ST >= (new ScoreSaver(Application.persistentDataPath + "/gamesave")).levelScoreSaver[levelManager.CurrentLevel].score[9])
                    {
                        //上榜
                        HighScoreInputUI.theScore = ST;
                        GoToMenu(HighScoreInputUI.gameObject);
                    }
                    else
                    {
                        //没上榜
                        GoToMenu(LevelEndMenu);
                    }

                    
                }
                    break;
        }
    }
}
