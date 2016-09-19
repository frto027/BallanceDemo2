using UnityEngine;
using System.Collections;

public class MS_OptionKeyboard : MenuScript
{
    /*
     * 上下左右旋转俯视 0 1 2 3 4 5
     * 返回 6
     */
    public AudioSource ClickedAudio;
    public GameObject[] Lights;
    public ButtonScript[] Buttons;
    private int Selecting;

    public void OnEnable()
    {
        Selecting = 0;
        for (int i = 0; i < 7; ++i)
            Buttons[Selecting].OnSelected(false);
        Buttons[Selecting].OnSelected(true);
        RefreshScore();
    }

    public override void OnButtinSelected(int ButtinId)
    {
        if ((Selecting < 6 && Lights[Selecting].activeInHierarchy) || Selecting == ButtinId)
            return;
        Buttons[Selecting].OnSelected(false);
        if (Selecting != 6)
            Lights[Selecting].SetActive(false);
        Buttons[Selecting = ButtinId].OnSelected(true);
    }
    public override void OnButtinClicked(int ButtinId)
    {
        if (ClickedAudio.isPlaying)
            ClickedAudio.Stop();
        ClickedAudio.Play();
        if (ButtinId == 6)
        {
            BackToParent();
            return;
        }

        Buttons[Selecting].OnSelected(false);
        if (Selecting != 6)
            Lights[Selecting].SetActive(false);

        Buttons[Selecting = ButtinId].OnSelected(true);
        if (Selecting != 6)
            Lights[Selecting].SetActive(true);
    }
    public void RefreshScore()
    {
        ScoreSaver ss = new ScoreSaver(Application.persistentDataPath + "/gamesave");
        Buttons[0].SetText(KeyName.GetKeyName(ss.Key_Up));
        Buttons[1].SetText(KeyName.GetKeyName(ss.Key_Down));
        Buttons[2].SetText(KeyName.GetKeyName(ss.Key_Left));
        Buttons[3].SetText(KeyName.GetKeyName(ss.Key_Right));
        Buttons[4].SetText(KeyName.GetKeyName(ss.Key_Turn));
        Buttons[5].SetText(KeyName.GetKeyName(ss.Key_OverView));
    }
    public void Update()
    {
        if (Selecting == 6 || !Lights[Selecting].activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)||JoyListener.UGetUpDown)
                OnButtinSelected((Selecting + Buttons.Length - 1) % Buttons.Length);
            if(Input.GetKeyDown(KeyCode.DownArrow)||JoyListener.UGetDownDown)
                OnButtinSelected((Selecting + 1) % Buttons.Length);
            if (Input.GetKeyDown(KeyCode.Return)||(Selecting == 6 && Input.GetButtonDown("A")))
            {
                OnButtinClicked(Selecting);
                return;
            }
            if (Input.GetKeyDown(KeyCode.Escape)||Input.GetButtonDown("B"))
            {
                OnButtinClicked(6);
            }
        }

        if (Input.anyKeyDown&&(Selecting<6&&Lights[Selecting].activeInHierarchy))
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode == KeyCode.Mouse0 || keyCode == KeyCode.Mouse1 || keyCode == KeyCode.Mouse2 || keyCode == KeyCode.Mouse3 || keyCode == KeyCode.Mouse4 || keyCode == KeyCode.Mouse5 || keyCode == KeyCode.Mouse6)
                        continue;
                    Lights[Selecting].SetActive(false);
                    if (keyCode != KeyCode.Escape && keyCode != KeyCode.Return)
                    {
                        ScoreSaver ss = new ScoreSaver(Application.persistentDataPath + "/gamesave");
                        switch (Selecting)
                        {
                            case 0:
                                ss.Key_Up = keyCode;
                                break;
                            case 1:
                                ss.Key_Down = keyCode;
                                break;
                            case 2:
                                ss.Key_Left = keyCode;
                                break;
                            case 3:
                                ss.Key_Right = keyCode;
                                break;
                            case 4:
                                ss.Key_Turn = keyCode;
                                break;
                            case 5:
                                ss.Key_OverView = keyCode;
                                break;
                        }
                        ss.SaveToFile();
                        RefreshScore();
                    }
                    break;
                }
            }
        }
    }
}
