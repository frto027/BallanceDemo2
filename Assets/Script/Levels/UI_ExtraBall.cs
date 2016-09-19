using UnityEngine;
using System.Collections;

public class UI_ExtraBall : MonoBehaviour {
    public AudioSource ExtraBallAudio;

    public bool showed = false;

    public void SetShowTrue()
    {
        showed = true;
    }
    public void SetShowFalse()
    {
        showed = false;
    }
    public void PlayAudio()
    {
        if (ExtraBallAudio == null)
            return;
        if (ExtraBallAudio.isPlaying)
            ExtraBallAudio.Stop();
        ExtraBallAudio.Play();
    }
}
