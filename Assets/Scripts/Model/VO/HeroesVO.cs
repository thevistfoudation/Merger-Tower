using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesVO : BaseMutilVO
{
    public HeroesVO()
    {
        LoadDataByDirectories<BaseVO>("Heroes");
    }
}
