using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NPCCharEffectInfo
{
    public NPCCharaterInfo percent;
    public NPCCharaterInfo effect;
}

public class NPCCharEffect : BaseEffectActive<NPCCharEffectInfo>
{
    NPCCharaterController charater;

    NPCCharaterInfo prevCharInfo;

    public override void ActiveEffect()
    {
        charater = GetComponent<NPCCharaterController>();
        if (charater == null)
        {
            Destroy(this);
        }
        prevCharInfo = charater.NPCCharaterInfo * (EffectInfo.percent / 100) + EffectInfo.effect;
        charater.NPCCharaterInfo += prevCharInfo;
    }


    private void OnDestroy()
    {
        if (charater != null)
        {
            charater.NPCCharaterInfo -= prevCharInfo;
        }
    }
}
