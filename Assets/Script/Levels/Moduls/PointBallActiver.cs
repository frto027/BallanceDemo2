using UnityEngine;
using System.Collections;
/// <summary>
/// 用于激活分数球，使其跟随玩家
/// </summary>
public class PointBallActiver : MonoBehaviour
{
    public AudioSource ExtraStartAudio;
    public PointBallFollower[] Points;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() == F_Object.levelManager.Player)
        {
            foreach(PointBallFollower pf in Points)
            {
                pf.BallCome();
            }
        }
        ExtraStartAudio.Play();
        All_RigidBodyAutoReset ar = GetComponent<All_RigidBodyAutoReset>();
        if (ar != null)
        {
            if (ar.Sectors == null)
            {
                ar.Sectors = new int[] { -1 };
            }
            else
            {
                int i;
                for (i = 0; i < ar.Sectors.Length; i++)
                {
                    if (ar.Sectors[i] == F_Object.levelManager.CurrentSector)
                        ar.Sectors[i] = -1;//cancel points in current sector
                }
            }
        }
        Destroy(this);
    }
}
