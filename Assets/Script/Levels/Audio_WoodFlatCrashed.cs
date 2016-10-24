using UnityEngine;
using System.Collections;
/// <summary>
/// 木板拍击声音
/// 要求有colider
/// </summary>
public class Audio_WoodFlatCrashed : MonoBehaviour
{
    public float maxVolumSpeed = 6f;
    public void OnCollisionEnter(Collision collision)
    {
        float aimVolum = collision.impulse.magnitude;
        aimVolum = aimVolum > maxVolumSpeed ? 1 : aimVolum / maxVolumSpeed;
        AudioSource woodFlapAudio = F_Object.levelManager.WoodFlapAudio;

        if (woodFlapAudio.isPlaying)
        {
            if (woodFlapAudio.volume > aimVolum)
                return;
            woodFlapAudio.Stop();
        }
        woodFlapAudio.volume = aimVolum;
        woodFlapAudio.Play();
    }
}
