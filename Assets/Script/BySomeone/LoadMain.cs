using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadMain : MonoBehaviour {

	public void LoadMainMenu()
    {
        PlayerPrefs.SetInt("PlayBallance", 1);
        SceneManager.LoadScene("Scene/BallanceAndMenu");
    }
}
