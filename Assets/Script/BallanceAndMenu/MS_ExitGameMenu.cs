using UnityEngine;
using System.Collections;

public class MS_ExitGameMenu : MenuScript
{
    //两个Button，Yse为0,No为1,Yse在左
    public AudioSource ClickedAudio;
    public ButtonScript YesBT;
    public ButtonScript NoButton;
    bool Selected;

    public void OnEnable()
    {
        Selected = false;
        YesBT.OnSelected(false);
        NoButton.OnSelected(true);
        YesBT.ButtinId = 0;
        NoButton.ButtinId = 1;
    }
    public override void OnButtinSelected(int ButtinId)
    {
        if (ClickedAudio != null)
        {
            if (ClickedAudio.isPlaying)
                ClickedAudio.Stop();
            ClickedAudio.Play();
        }
        Selected = (ButtinId == 0);
        YesBT.OnSelected(Selected);
        NoButton.OnSelected(!Selected);
    }
    public override void OnButtinClicked(int ButtinId)
    {
        if (ClickedAudio != null)
        {
            if (ClickedAudio.isPlaying)
                ClickedAudio.Stop();
            ClickedAudio.Play();
        }
        if (ButtinId == 0)
        {
            Application.Quit();
        }
        else
        {
            BackToParent();
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)||JoyListener.UGetLeftDown)
        {
            OnButtinSelected(0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)||JoyListener.UGetRightDown)
        {
            OnButtinSelected(1);
        }
        if (Input.GetKeyDown(KeyCode.Return)||Input.GetButtonDown("A"))
        {
            OnButtinClicked(Selected ? 0 : 1);
        }
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetButtonDown("B"))
        {
            OnButtinClicked(1);
        }
    }
}
