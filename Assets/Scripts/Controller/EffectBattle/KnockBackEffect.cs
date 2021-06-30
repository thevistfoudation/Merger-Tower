using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CharacterController = LTAUnityBase.Base.Character.CharacterController;

public class KnockBackEffectInfo
{
    public float speed;
}

public class KnockBackEffect : BaseEffectActive<KnockBackEffectInfo>
{

    public override void ActiveEffect()
    {
        MoveController move = GetComponent<MoveController>();
        if (move == null)
        {
            Destroy(this);
            return;
        }
        move.Stop = true;
    }

    private void Update()
    {
        transform.position += Vector3.right * EffectInfo.speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        MoveController move = GetComponent<MoveController>();
        if (move != null)
        {
            move.Stop = false;
            return;
        }
        
    }
}
