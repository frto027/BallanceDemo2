using UnityEngine;
using System.Collections;

public class P_ExtraLife : MonoBehaviour
{
    public GameObject[] objsToBeDestoryed;
    public ParticleSystem t_particleSystem;
    public AudioSource BallBreakAudio;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() == F_Object.levelManager.Player)
        {
            F_Object.levelManager.ExtraLife++;
            foreach (GameObject go in objsToBeDestoryed)
                Destroy(go);
        }
        t_particleSystem.Play();
        BallBreakAudio.Play();
    }
}
