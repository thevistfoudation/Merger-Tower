using System;
using UnityEngine;

public interface IFighter
{

    FightInfo fightInfo
    {
        set;
        get;
    }

    float minAttackRange {
        get;
    }

    float maxAttackRange
    {
        get;
    }

    Vector3 FighterPos
    {
        get;
    }

    float TimeDelay
    {
        get;
    }

    bool CheckCanAttack(EntityController entity);
    void Attack(Transform target);
}
