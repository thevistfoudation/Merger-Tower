using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BaseBulletInfo
{
    public string objectShoot;
    public string objectEndShoot;
    public float timeDestroy;
}
[RequireComponent(typeof(AttackerController))]
[RequireComponent(typeof(AutoDestroy))]
[DisallowMultipleComponent]
public abstract class BaseBulletController : DamageObject, IAutoDestroy
{
    bool isAutoDestroy = false;

    public bool IsAutoDestroy { get => isAutoDestroy; }

    public float TimeCountDestroy { get => bulletInfo.timeDestroy; }

    protected Vector3 direction;

    protected Vector3 targetPos;

    Action<GameObject> m_EndShoot;

    BaseBulletInfo bulletInfo;

    GameObject objectBullet;

    public virtual BaseBulletInfo BulletInfo
    {
        set
        {
            bulletInfo = value;
            Debug.Log("bulletInfo " + value.objectShoot + " " + value.objectEndShoot);
            CreateObjectBullet(value.objectShoot);
        }
    }

    protected T GetBulletInfo<T>() where T : BaseBulletInfo
    {
        return (T)bulletInfo;
    }

    GameObject CreateObjectBullet(string path)
    {

        GameObject prefab = Resources.Load<GameObject>("Bullet/" + path);
        if (objectBullet != null) Destroy(objectBullet);
        objectBullet = Instantiate(prefab, transform);
        return objectBullet;
    }

    public Action<GameObject> EndShoot
    {
        set
        {
            m_EndShoot = value;
        }
    }

    public virtual void Shoot(Transform target)
    {
        this.targetPos = target.position;
        direction = this.targetPos - transform.position;
        transform.up = direction;
    }

    void FixedUpdate()
    {
        if (isAutoDestroy) return;
        if (IsEndShoot && IsAllowAction)
        {
            isAutoDestroy = true;
            CreateObjectBullet(bulletInfo.objectEndShoot);
            if (m_EndShoot != null)
            {
                m_EndShoot(gameObject);
            }
            return;
        }
        BulletUpdate();
    }

    protected abstract void BulletUpdate();

    protected abstract bool IsEndShoot { get; }
}
