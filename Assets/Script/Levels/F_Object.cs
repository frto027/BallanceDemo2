using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class F_Object : MonoBehaviour
{
    static public LevelManager levelManager;
    static List<F_Object> F_Objects;
    protected bool Reseting;//有些Object需要用多帧来重置，Reseting置为true，卡白屏，ResetUpdate会被调用

    static public void ResetAll()
    {
        //全部发出重置消息
        foreach (F_Object fo in F_Objects)
            fo.Reset();
    }
    static public bool HasReseting()
    {
        foreach (F_Object fo in F_Objects)
            if (fo.Reseting)
                return true;
        return false;
    }
    static public void ResetUpdateAll()
    {
        foreach (F_Object fo in F_Objects)
            if(fo.Reseting)
                fo.ResetUpdate();
    }
    static public void PauseAll(bool isPause)
    {
        //全部发出暂停消息
        foreach (F_Object fo in F_Objects)
            fo.Pause(isPause);
    }
    static public void SwitchSectorAll()
    {
        //通知小节组变更
        foreach (F_Object fo in F_Objects)
            fo.SwitchSector();
    }

    public virtual void Start()
    {
        if (levelManager == null)
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        //加入列表
        if (F_Objects == null)
            F_Objects = new List<F_Object>();
        F_Objects.Add(this);
        Reseting = false;
    }

    public virtual void OnDestroy()
    {
        //移出列表
        F_Objects.Remove(this);
    }

    virtual public void Reset()
    {
        //重置
    }
    virtual public void ResetUpdate()
    {
        //延迟重置
    }
    virtual public void Pause(bool isPause)
    {
        //true为需要暂停
    }
    virtual public void SwitchSector()
    {
        //切换小节组，levelManager.CurrentLevel
    }
}
