using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.Character;
public class EnemyController : NPCCharaterController,IOnCharacterIdle
{
    Vector3 enemyDirection = Vector3.left;

    public Vector3 EnemyDirection
    {
        private get
        {
            return enemyDirection;
        }
        set
        {
            enemyDirection = value;
        }
    }

    public override Vector3 Direction {
        set
        {
            Vector3 enemyDirection = new Vector3(
                value.x,
                0,0
                );
            base.Direction = enemyDirection;
        }

    }

    public void Idle()
    {
        if (target == null)
        {
            Direction = EnemyDirection;
            Main.SetState(CharacterState.Move);
        }
    }

    public override void OnMove(Vector3 direction)
    {
        if (target == null)
        {
            Direction = EnemyDirection;
        }
    }

    public override void GetTarget(EntityController entity)
    {
        if (NPC.Target == null || NPC.Target != this) return;
        base.GetTarget(entity);
    }
}
