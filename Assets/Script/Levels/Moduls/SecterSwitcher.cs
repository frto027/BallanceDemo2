using UnityEngine;
using System.Collections;
/// <summary>
// 小节组切换碰撞箱，要求必须有trigger属性的colider，当当前小节为ActiveSector时，玩家球在进入此区域后将自动切换至下一小节
/// </summary>
public class SecterSwitcher : F_Object
{
    /// <summary>
    /// 此小节出生球的相对坐标
    /// </summary>
    public Vector3 rebirthPosition;
    /// <summary>
    /// 在哪个小节使这个碰撞箱生效？
    /// </summary>
    public int ActiveSector;

    /// <summary>
    /// 切换小节是否有声音？
    /// </summary>
    public bool haveSound = true;
    public new void Start()
    {
        base.Start();
        if (levelManager.CurrentSector != ActiveSector)
            gameObject.SetActive(false);
    }

    public override void SwitchSector()
    {
        gameObject.SetActive(levelManager.CurrentSector == ActiveSector);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<F_Player>() == levelManager.Player)
        {
            levelManager.RebirthPosition = GetComponent<Transform>().position + rebirthPosition;
            levelManager.NextSector();
            if (haveSound)
            {
                if (levelManager.SectorEndDongAudio.isPlaying)
                    levelManager.SectorEndDongAudio.Stop();
                levelManager.SectorEndDongAudio.Play();
            }
        }
    }
}
