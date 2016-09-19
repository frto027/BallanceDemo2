using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BM_LevelEndMenu : ButtonsMenu
{
    public GameObject HighscoreMenu;

    public new void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().CurrentLevel >= 12)
            ButtonNames[3] = "";
        base.OnEnable();
    }

    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);
        switch (ButtinId)
        {
            case 0:
                SceneManager.LoadScene("Scene/Loading");
                break;
            case 1:
                GoToMenu(HighscoreMenu);
                break;
            case 2:
                PlayerPrefs.SetInt("PlayBallance", 0);
                SceneManager.LoadScene("Scene/BallanceAndMenu");
                break;
            case 3:
                if (ButtonNames[3] == "")
                    goto case 2;
                PlayerPrefs.SetString("LoadLevel", "Scene/Levels/Level" + (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().CurrentLevel + 1+1).ToString());
                PlayerPrefs.SetInt("LoadLevelINT", GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().CurrentLevel + 1 + 1);
                SceneManager.LoadScene("Scene/Loading");
                break;
        }
    }
}
