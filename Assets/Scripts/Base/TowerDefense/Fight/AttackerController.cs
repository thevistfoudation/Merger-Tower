using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class AttackerController : MonoBehaviour
{
    public void AttackTarget(GameObject target,RealDamage damage)
    {
        IHit[] hits = target.GetComponents<IHit>();
        if (hits != null)
        {
            foreach(IHit hit in hits)
            {
                hit.Hit(damage);
            }
            IAttacker[] attackers = GetComponents<IAttacker>();
            if (attackers != null)
            {
                foreach(IAttacker attacker in attackers)
                {
                    attacker.MoreAttack(target, damage);
                }
            }
        }
    }
}
