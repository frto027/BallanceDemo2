using UnityEngine;
using System.Collections;

public class UI_AddScore : MonoBehaviour {
    public AudioSource AddScoreAudio;
    public void PlayAudio()
    {
        if (AddScoreAudio == null)
            return;
        if (AddScoreAudio.isPlaying)
            AddScoreAudio.Stop();
        AddScoreAudio.Play();
    }
}
