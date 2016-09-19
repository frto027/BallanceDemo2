using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BM_PauseMenu : ButtonsMenu {
    public GameObject HighscoreMenu;
    public override void OnButtinClicked(int ButtinId)
    {
        base.OnButtinClicked(ButtinId);
        switch (ButtinId)
        {
            case 0:
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().Pause(false);
                gameObject.SetActive(false);
                break;
            case 1:
                GoToMenu(HighscoreMenu);
                break;
            case 2:
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().Pause(false);
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().Death();
                goto case 0;
            case 3:
                SceneManager.LoadScene("Scene/Loading");
                break;
            case 4:
                PlayerPrefs.SetInt("PlayBallance", 0);
                SceneManager.LoadScene("Scene/BallanceAndMenu");
                break;
        }
    }
}
