using UnityEngine;
using System;

[System.Serializable]
public class BulletInfo : BaseBulletInfo
{
    public float speed;
}


public class BulletController : BaseBulletController
{
    MoveController moveController;

    protected T GetMoveController<T>() where T : MoveController
    {
        if (moveController == null)
             moveController = gameObject.AddComponent<T>();
        return (T)moveController;
    }

    public override BaseBulletInfo BulletInfo
    {
        set
        {
            base.BulletInfo = value;
            GetMoveController<MoveController>().Speed = GetBulletInfo<BulletInfo>().speed;
        }
    }

    protected override bool IsEndShoot => Vector3.Distance(transform.position,targetPos) <= 0.1f * GetMoveController<MoveController>().Speed;

    protected override void BulletUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.1f);
        DealDamage(hit);
        GetMoveController<MoveController>().Move(direction);
    }
}
