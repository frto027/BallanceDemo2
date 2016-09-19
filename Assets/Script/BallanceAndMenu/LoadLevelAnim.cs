using UnityEngine;
using System.Collections;

public class LoadLevelAnim : MonoBehaviour {

	public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene/Loading");
    }
}
