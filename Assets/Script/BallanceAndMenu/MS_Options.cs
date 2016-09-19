using UnityEngine;
using System.Collections;

public class MS_Options : ButtonsMenu {
    public GameObject OptionControl;
    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);
        switch (ButtinId)
        {
            case 1:
                GoToMenu(OptionControl);
                break;
            case 3:
                BackToParent();
                break;
        }
    }
}
