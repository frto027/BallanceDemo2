using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BM_DeathMenu : ButtonsMenu  {
    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);
        switch (ButtinId)
        {
            case 0:
                SceneManager.LoadScene("Scene/Loading");
                break;
            case 1:
                PlayerPrefs.SetInt("PlayBallance", 0);
                SceneManager.LoadScene("Scene/BallanceAndMenu");
                break;
        }
    }
}
