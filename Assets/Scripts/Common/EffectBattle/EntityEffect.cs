using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffectInfo
{
    public EntityInfo percent;
    public EntityInfo effect;
}

public class EntityEffect : BaseEffectActive<EntityEffectInfo>
{
    EntityController entity;

    EntityInfo prevEntityInfo;

    public override void ActiveEffect()
    {
        entity = GetComponent<EntityController>();
        if (entity == null)
        {
            Destroy(this);
        }
        prevEntityInfo = (entity.Info * (EffectInfo.percent / 100) + EffectInfo.effect);
        entity.Info += prevEntityInfo;
    }
        

    private void OnDestroy()
    {
        if (entity != null)
        {
            entity.Info -= prevEntityInfo;
        }
    }
}
