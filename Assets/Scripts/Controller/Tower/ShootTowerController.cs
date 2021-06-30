
using UnityEngine;

[RequireComponent(typeof(AutoTargetController), typeof(ShootController),typeof(NonEntityController))]
public class ShootTowerController : TowerController<ShootInfo>,IGetTargetAuto
{
    public override NonEntityController NonEntityController
    {
        get
        {
            if (nonEntityController == null)
            {
                nonEntityController = GetComponent<NonEntityController>();
            }
            return nonEntityController;
        }
    }

    private void Start()
    {
        NonEntityController.SetLevel(1);
    }

    IFighter fighter;

    protected IFighter Fighter {
        get
        {
            if (fighter == null)
            {
                fighter = GetComponent<IFighter>();
            }
            return fighter;
        }
    }

    public float MinTargetRange => Fighter.minAttackRange;

    public float MaxTargetRange => Fighter.maxAttackRange;

    public void GetTarget(EntityController entity)
    {
        if (entity != null)
            Fighter.Attack(entity.transform);
    }
}
