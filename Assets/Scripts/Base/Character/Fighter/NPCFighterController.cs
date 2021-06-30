using LTAUnityBase.Base.Character;
using UnityEngine;
[RequireComponent(typeof(AutoTargetController))]
public class NPCFighterController : CharacterFighter, IGetTargetAuto
{
    Transform tran_Target;

    public Transform Target
    {
        get
        {
            return tran_Target;
        }
    }

    public float MinTargetRange => Fighter.minAttackRange;

    public float MaxTargetRange => Fighter.maxAttackRange;

    public virtual void GetTarget(EntityController entity)
    {
        tran_Target = entity.transform;
        Main.SetState(CharacterState.Attack);
    }

    public override void Attacked()
    {
        Fighter.Attack(tran_Target);
    }

    public override void SetState(CharacterState state)
    {
        Vector3 direction = tran_Target.position - this.transform.position;

        if (direction.y > 0)
            StartAttack("Attack Up");
        else
            StartAttack("Attack Down");

        if (direction.x > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
