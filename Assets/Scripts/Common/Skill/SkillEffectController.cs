using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectInfo : BattleEffectInfo
{
    public int level;
}

public class SkillEffectController : EffectBattleController
{
    public Action endSkill;

    public Action EndSkill
    {
        set
        {
            endSkill = value;
        }
    }

    protected override EffectInfo GetEffectInfo(BattleEffectInfo info)
    {
        SkillEffectInfo skillEffectInfo = (SkillEffectInfo)info;
        return DataController.Instance.skillsVO.GetData<EffectInfo>(name+"_"+info.nameEffect,skillEffectInfo.level);
    }

    protected override void OnDestroy()
    {
        if (endSkill != null)
            endSkill();
        base.OnDestroy();
    }
}
