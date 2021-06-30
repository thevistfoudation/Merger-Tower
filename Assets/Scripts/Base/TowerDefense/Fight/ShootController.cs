using UnityEngine;
[System.Serializable]
public class ShootInfo : FightInfo
{
    public string typeBullet;
    public object bulletInfo;
}

public class ShootController : FightController<ShootInfo>
{
    [SerializeField]
    Transform tran_Shoot;

    protected override void Attack(Transform target, RealDamage damge)
    {
        BaseBulletController bullet;
        Debug.Log(Info.bulletInfo == null);
        bullet = Creater.Instance.createBullet(tran_Shoot,Info.bulletInfo.GetType());

        bullet.BulletInfo = (BaseBulletInfo)Info.bulletInfo;
        bullet.tag = tag;
        bullet.Damage = Damage.realDamage;
        bullet.EndShoot = EndShoot;
        if (Info.battleEffects != null)
        {
            EffectAttackController effectAttackController = bullet.GetComponent<EffectAttackController>();
            if (effectAttackController == null)
                effectAttackController = bullet.gameObject.AddComponent<EffectAttackController>();
            effectAttackController.battleEffects = Info.battleEffects;
        }
        bullet.Shoot(target);
       
        
    }

    void BeforeShoot(GameObject bullet)
    {
       
        
    }

    void EndShoot(GameObject bullet)
    {
        if (Info.splashInfo != null)
        {
            SplashController splash = bullet.AddComponent<SplashController>();
        }
    }
}
