using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
using System;

[DisallowMultipleComponent]
public class CreateController : MonoBehaviour {

    [SerializeField]
    GameObject prefab;

   public BaseBulletController createBullet(Transform shootPos,Type typeBullet)
    {
        GameObject bullet = Instantiate(prefab, shootPos.position, shootPos.rotation);
        if (typeBullet == typeof(BulletInfo))
        {
            return bullet.AddComponent<BulletController>();
        }

        if (typeBullet == typeof(ArcBulletInfo))
        {
            return bullet.AddComponent<ArcBulletController>();
        }

        return bullet.AddComponent<LaserController>();
    }

    public T creatEntityPlayer<T>(T prefab,Vector3 pos,int level) where T : EntityController
    {
        T objectPlayer = Instantiate(prefab, pos, prefab.transform.rotation);
        objectPlayer.name = prefab.name;
        objectPlayer.gameObject.AddComponent<NPCCharaterController>();
        objectPlayer.gameObject.AddComponent<NPCFighterController>();
        objectPlayer.tag = "Player";
        objectPlayer.GetComponent<NonEntityController>().SetLevel(level);
        return objectPlayer;
    }

    public T creatEntityEnemy<T>(T prefab, Vector3 pos) where T : EntityController
    {
        T objectPlayer = Instantiate(prefab, pos, prefab.transform.rotation);
        objectPlayer.name = prefab.name;
        objectPlayer.gameObject.AddComponent<EnemyController>();
        objectPlayer.gameObject.AddComponent<EnemyFightController>();
        objectPlayer.tag = "Enemy";
        return objectPlayer;
    }
    public T createTower<T>(T prefab, Vector3 pos, int level) where T : NonEntityController
    {
        T objectPlayer = Instantiate(prefab, pos, prefab.transform.rotation);
        objectPlayer.name = prefab.name;
        objectPlayer.GetComponent<NonEntityController>().SetLevel(level);
        return objectPlayer;
    }
}

public class Creater : SingletonMonoBehaviour<CreateController>
{

}
