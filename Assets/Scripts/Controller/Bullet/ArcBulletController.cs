using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBulletInfo : BulletInfo
{
    public float height;
}

public class ArcBulletController : BulletController
{
    //float height;

    //Vector3 directionY;

    //float speedY = 0;

    protected override bool IsAllowAction {
        get
        {
            return GetMoveController<ArcMoveController>().SpeedY < 0;
        }
    }

    public override BaseBulletInfo BulletInfo { 
            set{
                GetMoveController<ArcMoveController>().Height = ((ArcBulletInfo)value).height;
                base.BulletInfo = value;
            }
    }
    //protected override bool IsEndShoot => Mathf.Abs(transform.position.y - targetPos.y) <= 0.1f * GetMoveController<MoveController>().Speed;
    public override void Shoot(Transform target)
    {
        ArcMoveController arcMoveController = GetMoveController<ArcMoveController>();
        base.Shoot(target);
        float time = 1 / (arcMoveController.Speed * Time.fixedDeltaTime);
        time /= 2;
        arcMoveController.SetSpeedY(time);
    }

}
