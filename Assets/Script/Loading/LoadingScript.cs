using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class DeviceType
{
    public enum Type { KeyBoard ,TouchScreen,Joystick};
}

public class LoadingScript : MonoBehaviour {
    public RectTransform progressBar;
    public Text NameText;
    public Text InfoText;
    public Text TipText;
    private AsyncOperation LoadHandle;
    private ScoreSaver scoreSaver;
	// Use this for initialization
	void Start () {
        string LevelName = PlayerPrefs.GetString("LoadLevel");
        int LevelCount = PlayerPrefs.GetInt("LoadLevelINT");
        switch (LevelCount)
        {
            case 1:
                NameText.text = "Level 1";
                InfoText.text = "Testing...";
                break;
            default:
                NameText.text = "关卡" + LevelCount.ToString();
                InfoText.text = "暂无信息";
                break;
        }
        scoreSaver = new ScoreSaver(Application.persistentDataPath + "/gamesave");
        TipText.text = "点击屏幕/键盘<i>" + KeyName.GetKeyName(scoreSaver.Key_OverView) + "</i>(俯视)/手柄(<i>A</i>)继续";
        //然后异步加载场景
        StartCoroutine(MyLoad(LevelName));
	}
    public void SwitchToLevelScene()
    {
        GameObject go = GameObject.Find("DingAudio");
        if (go != null)
            DestroyObject(go);
        LoadHandle.allowSceneActivation = true;

    }
    IEnumerator MyLoad(string _levelName)
    {
        LoadHandle = SceneManager.LoadSceneAsync(_levelName);
        if(LoadHandle != null)
            LoadHandle.allowSceneActivation = false;
        yield return LoadHandle;
    }
	void SetProgress(float progress)
    {
        Vector2 v2 = progressBar.anchorMax;
        v2.x = progress;
        progressBar.anchorMax = v2;
    }
	// Update is called once per frame
	void Update () {
        if(LoadHandle == null)
        {
            TipText.text = "关卡加载失败，点击屏幕或按<i>Esc/B（手柄）</i>键返回";
        }else 
        if (LoadHandle.isDone)
            SetProgress(1f);
        else
            SetProgress(LoadHandle.progress);

        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetButtonDown("B"))
        {

            GameObject go = GameObject.Find("DingAudio");
            if (go != null)
                DestroyObject(go);
            PlayerPrefs.SetInt("PlayBallance", 0);
            SceneManager.LoadScene("Scene/BallanceAndMenu");
        }


        if (Input.GetKeyDown(scoreSaver.Key_OverView))
        {
            PlayerPrefs.SetInt("DeviceType", (int)DeviceType.Type.KeyBoard);
            GetComponent<Animator>().SetTrigger("Ok");
        }
        if (Input.GetButtonDown("A"))
        {
            PlayerPrefs.SetInt("DeviceType", (int)DeviceType.Type.Joystick);
            GetComponent<Animator>().SetTrigger("Ok");
        }

        if (Input.touchCount > 0)
        {
            if(LoadHandle == null)
            {
                PlayerPrefs.SetInt("PlayBallance", 0);
                SceneManager.LoadScene("Scene/BallanceAndMenu");
                return;
            }
            PlayerPrefs.SetInt("DeviceType", (int)DeviceType.Type.TouchScreen);
            GetComponent<Animator>().SetTrigger("Ok");
        }
	}
}
