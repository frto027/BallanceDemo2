using UnityEngine;
using System.Collections;

public class BM_OptionControl : ButtonsMenu {
    public GameObject OptionKeyBoard;
    public GameObject OptionTouchBoard;
    public GameObject OptionJoystick;
    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);
        switch (ButtinId)
        {
            case 0:
                GoToMenu(OptionKeyBoard);
                break;
            case 1:
                GoToMenu(OptionTouchBoard);
                break;
            case 2:
                GoToMenu(OptionJoystick);
                break;
            case 3:
                BackToParent();
                break;
        }
    }
}
