using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsVO : BaseMutilVO
{
    public SkillsVO()
    {
        LoadData<BaseVO>("Skills");
    }
}
