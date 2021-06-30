using UnityEngine;

[DisallowMultipleComponent]
public partial class EntityController : NonEntityController, IHit
{
    public HPController hpController;

    protected IOnDie[] onDies;

    protected void Start()
    {
        entities.Add(this);
        if (hpController == null)
            hpController = GetComponentInChildren<HPController>();
        hpController.die += OnDie;
    }

    EntityInfo info;

    public EntityInfo Info
    {
        set
        {
            info = value;
        }
        get
        {
            return info;
        }
    }

    public EntityInfo EntityInfo
    {
        set
        {
            info = value;
            if (hpController != null)
                hpController.SetHP(value.HP);
        }
    }

    public void StealHeal(float damage)
    {
        hpController.Heal(damage * info.lifeSteal);
    }

    public void Reflect(float realdamge)
    {
        hpController.TakeDamage(realdamge * info.reflect);
    }

    public void Hit(RealDamage damage)
    {
        float physicDamage = damage.physicDamage;
        float magicDamage = damage.magicDamage;
        float pureDamage = damage.pureDamage;
        
        if (damage.own != null)
        {
            damage.own.Reflect(physicDamage + magicDamage + pureDamage);
        }
        physicDamage *= 100 / (100 + info.armor);

        magicDamage *= 100 / (100 + info.magicResistance);

        CritDamage(ref physicDamage, damage);
        CritDamage(ref magicDamage, damage);
        CritDamage(ref pureDamage, damage);

        float realDamage = physicDamage + magicDamage + pureDamage;

        IEffectDamage[] effectDamages = GetComponents<IEffectDamage>();
        if (effectDamages != null)
        {
            foreach(IEffectDamage effectDamage in effectDamages)
            {
                realDamage += effectDamage.EffectDamage(damage);
            }
        }
        if (damage.own != null)
        {
            damage.own.StealHeal(realDamage);
        }
        hpController.TakeDamage(realDamage);
    }

    void CritDamage(ref float damage,in RealDamage info)
    {
        float crit = Random.Range(1f, 100f);
        if (crit <= info.critRate)
        {
            damage *= (float)info.critDamage / 100;
        }
    }

    void OnDie()
    {
        entities.Remove(this);
        onDies = GetComponents<IOnDie>();
        if (onDies != null)
        {
            foreach(IOnDie onDie in onDies)
            {
                onDie.OnDie();
            }
        }
    }
}
