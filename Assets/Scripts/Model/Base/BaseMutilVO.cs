using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
public class BaseMutilVO
{
    public Dictionary<string, BaseVO> dic_Data = new Dictionary<string, BaseVO>();
    Dictionary<string, BaseVO> dic_HeroesData = new Dictionary<string, BaseVO>();

    protected void LoadData<T>(string dataName) where T : BaseVO, new()
    {
        TextAsset[] texts = Resources.LoadAll<TextAsset>("Data/" + dataName);

        foreach (TextAsset text in texts)
        {
            T data = new T();
            data.LoadData(text);
            dic_Data.Add(text.name, data);
        }
    }

    protected void LoadDataByDirectories<T>(string dataName) where T : BaseVO, new()
    {
        string combinePath = Path.Combine(Application.dataPath + "/Resources/Data/", dataName);
        string[] allFiles = Directory.GetFiles(combinePath, "*.*", SearchOption.AllDirectories);
        if (allFiles == null || allFiles.Length == 0) return;
        int length = allFiles.Length;
        for (int i = 0; i < length; i++)
        {
            string filePath = allFiles[i];
            filePath = filePath.Replace("\\","/");
            if (!filePath.EndsWith(".json")) continue;
            string fileName = filePath.Substring(filePath.IndexOf("Data"));
            fileName = fileName.Substring(0, fileName.Length - 5);
            TextAsset text = Resources.Load<TextAsset>(fileName);
            T data = new T();
            data.LoadData(text);
            dic_HeroesData.Add(text.name, data);
        }   
    }

    public virtual T GetData<T>(string type, int level)
    {
        return dic_Data[type].GetData<T>(level);
    }

    public T[] GetDataHeroesByName<T>(string file_name)
    {
        return dic_HeroesData[file_name].GetDatas<T>();
    }
}
