using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamageEffect
{
    public float percent;
    public float damage;
}
[System.Serializable]
public class MoreDamgeEffectInfo
{
    public DamageEffect physicDamage;
    public DamageEffect magicDamage;
    public DamageEffect pureDamage;
}

public class MoreDamageEffect : BaseEffectActive<MoreDamgeEffectInfo>,IEffectDamage
{
    bool isActive = false;
    public override void ActiveEffect()
    {
        isActive = true;
    }

    public float EffectDamage(RealDamage damage)
    {
        if (!isActive) return 0;

        return    GetDamage(damage.physicDamage,EffectInfo.physicDamage)
                + GetDamage(damage.magicDamage,EffectInfo.magicDamage)
                + GetDamage(damage.pureDamage,EffectInfo.pureDamage);
    }

    float GetDamage(float damage,DamageEffect damageEffect)
    {
        return damage * ((float)damageEffect.percent/100) + damageEffect.damage;
    }
}
