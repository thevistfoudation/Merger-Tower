using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
public class FightersVO : BaseMutilVO
{
    public FightersVO()
    {
        LoadData<BaseVO>("Fighter");
    }

    public override T GetData<T>(string type, int level)
    {
        T result = base.GetData<T>(type, level);
        if (typeof(T) == typeof(ShootInfo))
        {
            ShootInfo shootInfo = result as ShootInfo;
            JSONObject json = dic_Data[type].GetData(level);
            shootInfo.bulletInfo = JsonUtility.FromJson(json["bulletInfo"].ToString(), Type.GetType(shootInfo.typeBullet));
        }
        return result;
    }
}
