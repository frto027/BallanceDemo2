using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    private Text theText;
    public Animator ScoreLightAnim;
    private LevelManager levelManager;
    private float Score;
    // Use this for initialization
    void Start()
    {
        theText = GetComponent<Text>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        Score = levelManager.Score;
        theText.text = ((int)Score).ToString();
    }

    public void FixedUpdate()
    {
        if (levelManager.Score >0 && levelManager.Score > Score)
            ScoreLightAnim.SetTrigger("Light");
        Score = levelManager.Score;
        theText.text = ((int)Score).ToString();
    }
}
