using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInfo
{
    public int numTarget = 0;
}

[RequireComponent(typeof(MeleeController),typeof(AutoTargetsController))]
public class SplashController : MonoBehaviour,IGetTargetsAuto
{
    SplashInfo info;

    public SplashInfo Info
    {
        set
        {
            info = value;
        }
        protected get
        {
            return info;
        }
    }

    MeleeController meleeController;

    MeleeController MeleeController
    {
        get
        {
            if (meleeController == null)
            {
                meleeController = GetComponent<MeleeController>();
            }
            return meleeController;
        }
    }

    public int numTarget => Info.numTarget;

    public float MinTargetRange => MeleeController.minAttackRange;

    public float MaxTargetRange => MeleeController.maxAttackRange;

    public void GetTarget(List<EntityController> entities)
    {
        foreach(EntityController entity in entities)
        {
            MeleeController.Attack(entity.transform);
        }
    }

}
