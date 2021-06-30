using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetTargetsAuto : IGetTargetRange
{
    int numTarget
    {
        get;
    }

    void GetTarget(List<EntityController> entities);
}
