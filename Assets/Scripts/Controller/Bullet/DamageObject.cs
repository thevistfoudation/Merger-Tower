using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AttackerController))]
public class DamageObject : MonoBehaviour
{
    GameObject beforeTarget;

    AttackerController attacker;

    AttackerController Attacker
    {
        get
        {
            if (attacker == null)
            {
                attacker = GetComponent<AttackerController>();
            }
            return attacker;
        }
    }

    RealDamage damage;

    public RealDamage Damage
    {
        set
        {
            damage = value;
        }
    }


    protected virtual bool IsAllowAction
    {
        get
        {
            return true;
        }
    }

    protected void DealDamage(RaycastHit2D hit)
    {
        if (hit.transform != null && hit.transform.tag != tag && IsAllowAction)
        {
            GameObject target = hit.transform.gameObject;
            if (beforeTarget == target) return;
            beforeTarget = target;
            Attacker.AttackTarget(target, damage);
        }
    }
}
