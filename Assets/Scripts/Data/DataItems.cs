public class DataItem
{
   
}
[System.Serializable]
public struct DatasItemHeroInGameplay
{
    public DataItemHeroInGameplay[] data;
}

[System.Serializable]
public class DataItemHeroInGameplay
{
    public int id { get; set; }
    public string name { get; set; }
    public string icon { get; set; }
    public long price { get; set; }
}

