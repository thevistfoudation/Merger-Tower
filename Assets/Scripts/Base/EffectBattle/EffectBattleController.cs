using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class SubInfo
{
    public string type;
    public object data;
}
[System.Serializable]
public class EffectInfo
{
    public string pathEffect = "";
    public SubInfo[] battleEffects;
}
[System.Serializable]
public class BattleEffectInfo
{
    public float percentEffect = 100f;
    public float timeEffect =  0;
    public string nameEffect = "";
}

public class EffectBattleController : MonoBehaviour 
{

    private float timeAffect;

    bool isActive = false;

    GameObject effect;

    protected bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    List<MonoBehaviour> effectBattles = new List<MonoBehaviour>();

    string effectName;

    public string EffectName
    {
        get
        {
            return effectName;
        }
    }

    float timeEffect;

    public void ResetTime()
    {
        timeAffect = timeEffect;
    }

    public BattleEffectInfo Info
    {
        set
        {
            timeAffect = value.timeEffect;
            timeEffect = value.timeEffect;
            effectName = value.nameEffect;
            EffectInfo effectInfo = GetEffectInfo(value);
            
            GameObject prefabEffect = Resources.Load<GameObject>(effectInfo.pathEffect);
            if (prefabEffect != null)
            {
                effect = Instantiate(prefabEffect, transform);
                effect.transform.localPosition = Vector3.zero;
            }

            SubInfo[] infos = effectInfo.battleEffects;
            foreach (SubInfo info in infos)
            {
                IEffectBattle effectBattle = (IEffectBattle)gameObject.AddComponent(Type.GetType(info.type));
                if (effectBattle == null) continue;
                effectBattle.Info = info.data;
                effectBattles.Add((MonoBehaviour)effectBattle);
                effectBattle.ActiveEffect();
            }

            isActive = true;
        }
    }

    protected virtual EffectInfo GetEffectInfo(BattleEffectInfo info)
    {
        return DataController.Instance.effectBattlesVO.GetEffectInfo(info.nameEffect);
    }

    void Update()
    {
        if (!isActive) return;
        if (timeAffect <= 0) return;
        timeAffect -= Time.deltaTime;
        if (timeAffect <= 0)
        {
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (effect != null)
            Destroy(effect);
        foreach(MonoBehaviour effectBattle in effectBattles)
        {
            if (effectBattle != null)
                Destroy(effectBattle);
        }
    }


}
