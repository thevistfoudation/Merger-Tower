using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEffectInfo
{
    public FightInfo percent;
    public FightInfo effect;
}

public class FightEffect : BaseEffectActive<FightEffectInfo>
{
    IFighter fight;

    FightInfo prevfightInfo;

    private void OnDestroy()
    {
        if (fight != null)
        {
            fight.fightInfo -= prevfightInfo;
        }
    }

    public override void ActiveEffect()
    {
        fight = GetComponent<IFighter>();
        if (fight == null)
        {
            Debug.Log("Don't find Fighter");
            Destroy(this);
        }
        prevfightInfo = fight.fightInfo * (EffectInfo.percent / 100) + EffectInfo.effect;
        fight.fightInfo += prevfightInfo;
    }
}
