using UnityEngine;
using SimpleJSON;
using System;
public class EffectBattlesVO : BaseVO
{
    public EffectBattlesVO()
    {
        LoadDataLocal("EffectBattle");
    }

    public EffectInfo GetEffectInfo(string EffectName)
    {
        EffectInfo effectInfo = new EffectInfo();
        JSONObject json = data[EffectName].AsObject;
        effectInfo.pathEffect = json["pathEffect"];

        JSONArray infos = json["battleEffects"].AsArray;
        SubInfo[] battleEffects = new SubInfo[infos.Count];
        for (int i = 0; i < infos.Count;i++)
        {
            battleEffects[i] = new SubInfo();
            battleEffects[i].type = infos[i]["type"];
            if (infos[i]["typeInfo"] == null) continue;
            string typeInfo = infos[i]["typeInfo"];
            battleEffects[i].data = JsonUtility.FromJson(infos[i]["data"].ToString(), Type.GetType(typeInfo));
        }
        effectInfo.battleEffects = battleEffects;
        return effectInfo;
    }
}
