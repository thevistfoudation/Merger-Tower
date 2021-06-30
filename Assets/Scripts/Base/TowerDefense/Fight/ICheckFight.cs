using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICheckFight
{
    bool checkFight(IFighter fighter,EntityController entity);
}
