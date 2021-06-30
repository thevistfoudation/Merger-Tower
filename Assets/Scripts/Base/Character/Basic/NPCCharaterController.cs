using UnityEngine;
using CharacterController = LTAUnityBase.Base.Character.CharacterController;

[System.Serializable]
public class NPCCharaterInfo
{
    public float targetRange = 0;
    public float speed = 0;

    public static NPCCharaterInfo operator +(NPCCharaterInfo a , NPCCharaterInfo b)
    {
        NPCCharaterInfo npc = new NPCCharaterInfo();
        npc.targetRange = a.targetRange + b.targetRange;
        npc.speed = a.speed + b.speed;
        return npc;
    }

    public static NPCCharaterInfo operator -(NPCCharaterInfo a, NPCCharaterInfo b)
    {
        NPCCharaterInfo npc = new NPCCharaterInfo();
        npc.targetRange = a.targetRange - b.targetRange;
        npc.speed = a.speed - b.speed;
        return npc;
    }

    public static NPCCharaterInfo operator * (NPCCharaterInfo a, NPCCharaterInfo b)
    {
        NPCCharaterInfo npc = new NPCCharaterInfo();
        npc.targetRange = a.targetRange * b.targetRange;
        npc.speed = a.speed * b.speed;
        return npc;
    }

    public static NPCCharaterInfo operator /(NPCCharaterInfo a, float b)
    {
        NPCCharaterInfo npc = new NPCCharaterInfo();
        if (b == 0) return npc;
        npc.targetRange = a.targetRange / b;
        npc.speed = a.speed / b;
        return npc;
    }
}

[DisallowMultipleComponent]
[RequireComponent(typeof(AutoTargetController),typeof(EntityController))]
public class NPCCharaterController : FearCharacter,IGetTargetAuto,IOnCharacterMove
{
    NPCFighterController npc;

    protected NPCFighterController NPC
    {
        get
        {
            if (npc == null)
            {
                npc = GetComponent<NPCFighterController>();
            }
            return npc;
        }
    }

    NPCCharaterInfo npcCharaterInfo;

    public NPCCharaterInfo  NPCCharaterInfo
    {
        set
        {
            MoveController.Speed = value.speed;
            npcCharaterInfo = value;
        }
        get
        {
            return npcCharaterInfo;
        }

    }

    protected Transform target;

    public Transform Target
    {
        get
        {
            return target;
        }
    }

    public float MinTargetRange => 0;

    public float MaxTargetRange => npcCharaterInfo.targetRange;

    public virtual void OnMove(Vector3 direction)
    {
        if (target == null)
        {
            Main.SetState(CharacterState.Idle);
        }
    }

    public virtual void GetTarget(EntityController entity)
    {
        if (NPC.Target != null)
        {
            target = null;
            return;
        }
        target = entity.transform;
        Direction = target.position - this.transform.position;
        Main.SetState(CharacterState.Move);
    }
}
