using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    SubInfo[] conditions;
    SubInfo[] skills;
}

public class SkillController : MonoBehaviour
{
    //ICondition[] conditions;

    SkillInfo info;
    public SkillInfo Info
    {
        set
        {
            info = value;
        }
    }
}
