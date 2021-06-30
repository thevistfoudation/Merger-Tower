using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using System;
public class BaseVO
{

    protected JSONNode data;
    //static Dictionary<string, JSONNode> datas = new Dictionary<string, JSONNode>();

    //public static void PreloadDatas<T>() where T : TextAsset
    //{
    //    string assetName = typeof(T).Name;
    //    JSONNode json;
    //    if (datas.TryGetValue(assetName, out json) == true)
    //    {
    //        return;
    //    }

    //    json = JSON.Parse(Resources.Load<TextAsset>("Data/" + assetName).text)["data"];
    //    if (json == null)
    //    {
    //        Debug.LogError("cannot load " + assetName);
    //        return;
    //    }

    //    datas[assetName] = json;
    //}

    public static JSONNode LoadData(string dataName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + dataName);

        return JSON.Parse(textAsset.text);
    }

    protected void LoadDataLocal(string dataName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + dataName);
        data = JSON.Parse(textAsset.text)["data"];
    }

    public void LoadData(TextAsset textAsset)
    {
        data = JSON.Parse(textAsset.text)["data"];
    }

    public T[] GetDatas<T>()
    {
        JSONArray array = data.AsArray;
        int length = array.Count;
        T[] t = new T[length];
        for (int i = 0; i < length; i++)
        {
            t[i] = JsonUtility.FromJson<T>(array[i].ToString());
        }
        
        return t;
    }

    public T GetData<T>(int level)
    {
        JSONArray array = data.AsArray;
        if (level > array.Count)
            return JsonUtility.FromJson<T>(array[array.Count - 1].ToString());
        return JsonUtility.FromJson<T>(array[level - 1].ToString());
    }

    public object GetData(string key,Type type)
    {
        JSONObject json = data.AsObject;
        if (json[key].IsNull) return null;

        return JsonUtility.FromJson(json[key].ToString(),type);
    }

    public JSONObject GetData(int level)
    {
        return data[level-1].AsObject;
    }
}
