using UnityEngine;
using System.Collections;

public class BM_OptionTouchScreen : ButtonsMenu
{

    public new void Start()
    {
        base.Start();
        base.OnButtinSelected(PlayerPrefs.GetInt("TouchKeyboardId", 0));
    }

    public new void OnEnable()
    {
        base.OnEnable();
        base.OnButtinSelected(PlayerPrefs.GetInt("TouchKeyboardId", 0));
    }

    public override void OnButtinSelected(int ButtinId)
    {
        //do nothing here
        //base.OnButtinSelected(ButtinId);
    }
    public override void OnButtinClicked(int ButtinId)
    {
        if (ButtinId == EscapeButton)
        {
            BackToParent();
            return;
        }
        base.OnButtinClicked(ButtinId);
        base.OnButtinSelected(ButtinId);
        PlayerPrefs.SetInt("TouchKeyboardId", ButtinId);
    }

}
