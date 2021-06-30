using UnityEngine;
[RequireComponent(typeof(AttackerController))]
public class MeleeController : FightController<FightInfo>
{
    AttackerController attacker;

    protected AttackerController Attacker
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
    protected override void Attack(Transform target, RealDamage damge)
    {
        Attacker.AttackTarget(target.gameObject, damge);
    }
}
