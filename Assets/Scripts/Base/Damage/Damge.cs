
using UnityEngine;
[System.Serializable]
public class Damage
{
    public float critRate = 0;
    public float critDamage = 0;
    public float minPhysicDamage = 0;
    public float maxPhysicDamage = 0;
    public float minMagicDamage = 0;
    public float maxMagicDamage = 0;
    public float minPureDamage = 0;
    public float maxPureDamage = 0;

    public static Damage operator+(Damage a, Damage b)
    {
        Damage damage           = new Damage();
        damage.critRate         = a.critRate        + b.critRate;
        damage.critDamage       = a.critDamage      + b.critDamage;

        damage.minPhysicDamage  = a.minPhysicDamage + b.minPhysicDamage;
        damage.maxPhysicDamage  = a.maxPhysicDamage + b.maxPhysicDamage;

        damage.minMagicDamage   = a.minMagicDamage  + b.minMagicDamage;
        damage.maxMagicDamage   = a.maxMagicDamage  + b.maxMagicDamage;

        damage.minPureDamage    = a.minPureDamage   + b.minPureDamage;
        damage.maxPureDamage    = a.maxPureDamage   + b.maxPureDamage;
        return damage;
    }

    public static Damage operator - (Damage a, Damage b)
    {
        Damage damage           = new Damage();
        damage.critRate         = a.critRate        - b.critRate;
        damage.critDamage       = a.critDamage      - b.critDamage;

        damage.minPhysicDamage  = a.minPhysicDamage - b.minPhysicDamage;
        damage.maxPhysicDamage  = a.maxPhysicDamage - b.maxPhysicDamage;

        damage.minMagicDamage   = a.minMagicDamage  - b.minMagicDamage;
        damage.maxMagicDamage   = a.maxMagicDamage  - b.maxMagicDamage;

        damage.minPureDamage    = a.minPureDamage   - b.minPureDamage;
        damage.maxPureDamage    = a.maxPureDamage   - b.maxPureDamage;
        return damage;
    }

    public static Damage operator * (Damage a, Damage b)
    {
        Damage damage = new Damage();
        damage.critRate = a.critRate * b.critRate;
        damage.critDamage = a.critDamage * b.critDamage;

        damage.minPhysicDamage = a.minPhysicDamage * b.minPhysicDamage;
        damage.maxPhysicDamage = a.maxPhysicDamage * b.maxPhysicDamage;

        damage.minMagicDamage = a.minMagicDamage * b.minMagicDamage;
        damage.maxMagicDamage = a.maxMagicDamage * b.maxMagicDamage;

        damage.minPureDamage = a.minPureDamage * b.minPureDamage;
        damage.maxPureDamage = a.maxPureDamage * b.maxPureDamage;
        return damage;
    }

    public static Damage operator / (Damage a, float b)
    {
        Damage damage = new Damage();
        if (b == 0) return damage;
        damage.critRate = (float) a.critRate / b;
        damage.critDamage = (float)a.critDamage / b;

        damage.minPhysicDamage = (float)a.minPhysicDamage / b;
        damage.maxPhysicDamage = (float)a.maxPhysicDamage / b;

        damage.minMagicDamage = (float)a.minMagicDamage / b;
        damage.maxMagicDamage = (float)a.maxMagicDamage / b;

        damage.minPureDamage = (float)a.minPureDamage / b;
        damage.maxPureDamage = (float)a.maxPureDamage / b;
        return damage;
    }

    public RealDamage realDamage
    {
        get
        {
            return new RealDamage(this);
        }
    }
}

[System.Serializable]
public class RealDamage
{
    public float critRate = 0;
    public float critDamage = 0;
    public float physicDamage = 0;
    public float magicDamage = 0;
    public float pureDamage = 0;
    public EntityController own;

    public static RealDamage operator + (RealDamage a,RealDamage b)
    {
        RealDamage realDamage = new RealDamage();
        realDamage.critRate = a.critRate + b.critRate;
        realDamage.critDamage = a.critDamage + b.critDamage;
        realDamage.physicDamage = a.physicDamage + b.physicDamage;
        realDamage.magicDamage = a.magicDamage+ b.magicDamage;
        realDamage.pureDamage = a.critRate + b.pureDamage;
        return realDamage;
    }

    public RealDamage()
    {

    }

    public RealDamage(Damage damage)
    {
        critRate = damage.critRate;
        critDamage = damage.critDamage;
        physicDamage = Random.Range(damage.minPhysicDamage, damage.maxPhysicDamage);
        magicDamage = Random.Range(damage.minMagicDamage, damage.maxMagicDamage);
        pureDamage = Random.Range(damage.minPureDamage, damage.maxPureDamage);
    }
}
