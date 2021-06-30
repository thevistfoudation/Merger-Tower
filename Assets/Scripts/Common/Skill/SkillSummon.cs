using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillSumonInfo
{
    public string objectSummonPath;
    public int numObject;
    public int level;
}
public class SkillSummon : BaseEffectActive<SkillSumonInfo>
{
    GameObject prefabObject;

    Transform skill_Pos;

    public override object Info {
        set {
            base.Info = value;
            prefabObject = Resources.Load<GameObject>(EffectInfo.objectSummonPath);
            skill_Pos = transform.Find("SkillPos");
            if (skill_Pos == null) skill_Pos = transform;
        }
    }

    public override void ActiveEffect()
    {
        if (prefabObject == null) return;
        for (int i = 0;i< EffectInfo.numObject;i++)
        {
            GameObject gameObject = Instantiate(prefabObject, transform);
            gameObject.transform.position = skill_Pos.position;
            gameObject.GetComponent<NonEntityController>().SetLevel(EffectInfo.level);
        }
    }
}
