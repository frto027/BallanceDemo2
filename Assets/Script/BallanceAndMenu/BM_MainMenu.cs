using UnityEngine;
using System.Collections;

public class BM_MainMenu :  ButtonsMenu {
    public GameObject Levelmenu;
    public GameObject Highscore;
    public GameObject Options;
    public GameObject Credits;
    public GameObject Exit;
    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);//play music
        switch (ButtinId)
        {
            //这里处理按钮按下事件
            case 0:
                GoToMenu(Levelmenu);
                break;
            case 1:
                GoToMenu(Highscore);
                break;
            case 2:
                GoToMenu(Options);
                break;
            case 3:
                GoToMenu(Credits);
                break;
            case 4:
                GoToMenu(Exit);
                break;
        }
    }
}
