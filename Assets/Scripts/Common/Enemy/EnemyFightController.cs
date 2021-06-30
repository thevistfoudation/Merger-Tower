using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightController : NPCFighterController
{
    public override void GetTarget(EntityController entity)
    {
        NPCFighterController npcFight = entity.GetComponent<NPCFighterController>();
        if (npcFight.Target == null || npcFight.Target != transform) return;
        base.GetTarget(entity);
    }

    protected override void HandleEndAttack()
    {
        if (Target ==  null)
        {
            GetComponent<EnemyController>().Direction = Vector3.left;
            Main.SetState(CharacterState.Move);
        }
        base.HandleEndAttack();
    }
}
