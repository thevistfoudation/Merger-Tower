using UnityEngine;
[System.Serializable]
public class TakeDamageEffectInfo
{
    public RealDamage percentDamageFollowHP;
    public RealDamage damage;
    public float timeDelayTakeDamage = 0;
}

public class TakeDamageEffect : BaseEffectActive<TakeDamageEffectInfo>
{
    float countDelay = 0;

    bool isActive = false;

    public override void ActiveEffect()
    {
        isActive = true;
    }

    void Update()
    {
        if (!isActive) return;
        RealDamage bonusDamage = new RealDamage();
        EntityController entity = GetComponent<EntityController>();
        if (entity != null)
        {
            float hp = entity.hpController.HP;
            bonusDamage.physicDamage =  hp*(float)EffectInfo.percentDamageFollowHP.physicDamage / 100;
            bonusDamage.magicDamage = hp*(float)EffectInfo.percentDamageFollowHP.magicDamage / 100;
            bonusDamage.pureDamage = hp*(float)EffectInfo.percentDamageFollowHP.pureDamage / 100;
        }
        if (countDelay >= EffectInfo.timeDelayTakeDamage)
        {
            countDelay = 0;
            IHit[] hits = GetComponents<IHit>();
            foreach(IHit hit in hits)
            {
                hit.Hit(EffectInfo.damage + bonusDamage);
            }
            

        }
        else
        {
            countDelay += Time.deltaTime;
        }
    }
}
