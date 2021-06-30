using System;
using UnityEngine;

public class LaserInfo : BaseBulletInfo
{
    public float timeDelayHit;
}
public class LaserController : BaseBulletController
{

    float countDownHit = 0;

    float countDownShoot = 0;

    protected override bool IsEndShoot {
        get
        {
            if (countDownShoot <= GetBulletInfo<LaserInfo>().timeDestroy)
            {
                countDownShoot += Time.deltaTime;
                return false;
            }
            return true;
        }
    }

    protected override void BulletUpdate()
    {
        if (direction == Vector3.zero) return;
        if (countDownHit < GetBulletInfo<LaserInfo>().timeDelayHit)
        {
            countDownHit += Time.deltaTime;
            return;
        }
        countDownHit = 0;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);
        if (hits == null) return;
        foreach (RaycastHit2D hit in hits)
        {
            DealDamage(hit);
        }
    }
}
