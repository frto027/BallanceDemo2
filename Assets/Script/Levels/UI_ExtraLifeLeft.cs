using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * 生命显示UI，自动识别、初始化、设置UI中生命值的显示
 * HP低于0为空，大于等于254不予以显示多余项，导致吃到生命求没有声音，但有统计
 */
public class UI_ExtraLifeLeft : MonoBehaviour
{
    private LevelManager levelManager;

    public float posx;
    private float buf1, buf2, buf3;
    private RectTransform theLeft;

    public Transform ParentExtraBall;
    private int Life;
    public Animator ExtraBallSample;
    private Animator[] UIBalls = new Animator[255];
    private UI_ExtraBall[] UIBallsScr = new UI_ExtraBall[255];
    public AudioSource ExtraBallShowAudio;
    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        theLeft = GetComponent<RectTransform>();
        Vector2 pos = theLeft.anchoredPosition;
        pos.x = buf1 = buf2 = buf3 = posx;
        theLeft.anchoredPosition = pos;

        Life = levelManager.ExtraLife;
        //init ui balls
        for(int i = 0; i < 255; ++i)
        {
            UIBalls[i] = Instantiate(ExtraBallSample);
            UIBalls[i].GetComponent<Transform>().SetParent(ParentExtraBall, false);
            UIBalls[i].GetComponent<UI_ExtraBall>().ExtraBallAudio = ExtraBallShowAudio;
            RectTransform rt = UIBalls[i].GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2(-40 - 29 * i, 36);

            UIBallsScr[i] = UIBalls[i].GetComponent<UI_ExtraBall>();
            UIBalls[i].SetBool("Show", i < Life);
            UIBalls[i].SetTrigger("Prefed");
        }
    }

    public void FixedUpdate()
    {
        buf1 = (buf1 - posx) * 0.6f + posx;
        buf2 = (buf2 - buf1) * 0.6f + buf1;
        buf3 = (buf3 - buf2) * 0.6f + buf2;
        Vector2 pos = theLeft.anchoredPosition;
        pos.x = buf3;
        theLeft.anchoredPosition = pos;
        
        for(int i = 0; i < levelManager.ExtraLife && i<255; i++)
        {
            if (UIBalls[i].GetBool("Show") != true)
                UIBalls[i].SetBool("Show", true);
        }
        for(int i = levelManager.ExtraLife > 0 ? levelManager.ExtraLife : 0; i < 255; i++)
        {
            if (UIBalls[i].GetBool("Show") != false)
                UIBalls[i].SetBool("Show", false);
        }
        int temp;
        for (temp = 254; temp >= 0; --temp)
        {
            if (UIBalls[temp].GetBool("Show") || UIBallsScr[temp].showed)
                break;
        }
        temp++;
        posx = -40 - 29 * temp;

    }
}
