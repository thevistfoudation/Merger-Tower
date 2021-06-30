
using UnityEngine;
[System.Serializable]
public class FightInfo
{
    public float attackSpeed = 0;

    public float minAttackRange = 0;

    public float maxAttackRange = 0;

    public Damage damage;

    public static FightInfo operator + (FightInfo a,FightInfo b)
    {
        FightInfo info = new FightInfo();

        info.minAttackRange = a.minAttackRange + b.minAttackRange;
        info.maxAttackRange = a.maxAttackRange + b.maxAttackRange;
        info.attackSpeed = a.attackSpeed + b.attackSpeed;
        info.damage = a.damage + b.damage;
        return info;
    }

    public static FightInfo operator - (FightInfo a, FightInfo b)
    {
        FightInfo info = new FightInfo();

        info.minAttackRange = a.minAttackRange - b.minAttackRange;
        info.maxAttackRange = a.maxAttackRange - b.maxAttackRange;
        info.attackSpeed = a.attackSpeed - b.attackSpeed;
        info.damage = a.damage - b.damage;
        return info;
    }

    public static FightInfo operator * (FightInfo a, FightInfo b)
    {
        FightInfo info = new FightInfo();
        info.minAttackRange = a.minAttackRange * b.minAttackRange;
        info.maxAttackRange = a.maxAttackRange * b.maxAttackRange;
        info.attackSpeed = a.attackSpeed * b.attackSpeed;
        info.damage = a.damage * b.damage;
        return info;
    }

    public static FightInfo operator /(FightInfo a, float b)
    {
        FightInfo info = new FightInfo();
        info.damage = a.damage / b;
        if (b == 0) return info;
        info.minAttackRange = (float)a.minAttackRange/ b;
        info.maxAttackRange = (float)a.maxAttackRange / b;
        info.attackSpeed = (float)a.attackSpeed / b;
        return info;
    }
    public BattleEffectInfo[] battleEffects;
    public SplashInfo splashInfo;
}

[DisallowMultipleComponent]
public abstract class FightController<T> : MonoBehaviour,IFighter where T : FightInfo
{
    T info;

    public FightInfo fightInfo
    {
        get
        {
            return info;
        }
        set
        {
            Info = info;
        }
    }

    public T Info
    {
        get
        {
            return info;
        }
        set
        {
            info = value;
            timeDelay = 1 / value.attackSpeed;
        }
    }

    float timeDelay;

    public float TimeDelay {
        get
        {
            return timeDelay;
        }
    }

    bool isAllowAttack = true;

    float countDown = 0;

    protected Damage Damage
    {
        get
        {
            return info.damage;
        }
    }

    EntityController entity;

    public EntityController Entity
    {
        get
        {
            if (entity == null)
            {
                entity = GetComponent<EntityController>();
            }
            return entity;
        }
    }

    public Vector3 FighterPos => transform.position;

    public float minAttackRange => fightInfo.minAttackRange;

    public float maxAttackRange => fightInfo.maxAttackRange;

    public void Attack(Transform target)
    {
        float distance = Vector3.Distance(target.position, this.transform.position);
        if (distance >= info.minAttackRange && distance <= info.maxAttackRange && isAllowAttack)
        {
            isAllowAttack = false;
            RealDamage realDamage = info.damage.realDamage;
            realDamage.own = Entity;
            Attack(target,realDamage);

        }
    }

    protected virtual void Update()
    {
        if (countDown < timeDelay)
        {
            countDown += Time.deltaTime;
            return;
        }
        countDown = 0;
        isAllowAttack = true;
    }

    protected abstract void Attack(Transform target,RealDamage damage);

    public bool CheckCanAttack(EntityController entity)
    {
        ICheckFight[] checkFights = GetComponents<ICheckFight>();

        bool result = true;

        foreach (ICheckFight checkFight in checkFights)
        {
            result = result && checkFight.checkFight(this, entity);
        }

        return result && (tag != entity.tag) && entity != this && isAllowAttack;
    }

    
}
