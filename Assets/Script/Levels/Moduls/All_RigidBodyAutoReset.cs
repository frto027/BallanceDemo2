using UnityEngine;
using System.Collections;
/// <summary>
/// 此类使某物体能够监听并响应Reset（重置）和SwitchSector（小节组切换）事件
/// 参数：
/// resourceDir：是物体的自身prefabs存放路径，物体重置以后将加载resourceDir指向的物体。
/// Sectors：小节组，是一个数组，一个物体可以同时隶属于多个小节组。
/// </summary>
public class All_RigidBodyAutoReset : F_Object
{
    public string resourceDir;
    private Vector3 initPos;
    private Quaternion initRot;
    public int [] Sectors = new int [0];
    public new void Start()
    {
        base.Start();
        initPos = GetComponent<Transform>().position;
        initRot = GetComponent<Transform>().rotation;
        gameObject.SetActive(isInSector());
    }
    public override void SwitchSector()
    {
        base.SwitchSector();
        gameObject.SetActive(false);
        if (isInSector())
        {
            Reset();
        }
        
    }
    public override void Reset()
    {
        if (!isInSector())
            return;

        GameObject nextObj = Instantiate(Resources.Load(resourceDir) as GameObject, initPos, initRot) as GameObject;
        All_RigidBodyAutoReset nextRBAR = nextObj.GetComponent<All_RigidBodyAutoReset>();
        if (nextRBAR != null)
        {
            nextRBAR.Sectors = Sectors;
        }
        Destroy(gameObject);
    }

    public bool isInSector(int NowSector = -1)
    {
        if (NowSector == -1)
            NowSector = levelManager.CurrentSector;
        if (Sectors == null || Sectors.Length == 0)
            return true;
        else
        {
            foreach (int sector in Sectors)
            {
                if (NowSector == sector)
                    return true;
            }
        }
        return false;
    }
}
