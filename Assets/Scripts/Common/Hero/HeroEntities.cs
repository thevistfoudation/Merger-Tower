[System.Serializable]
public struct RecordHeroInfo
{
    public string name;
    public string age;
    public string height;
    public string weight;
    public string gender;
    public string blood_group;
    public string birth;
    public string weapon;
    public string icon;
    public string description;
    public string[] classes;
    public Attr attr;
    public Skill[] skills;
    public struct Attr
    {
        public string atk;
        public string atk_speed;
        public string crit_chance;
        public string crit_damage;
    }

    public struct Skill
    {
        public int id;
        public string type;
        public string unlock;
        public string name;
        public string description;
    }
}

[System.Serializable]
public struct RecordHeroLevelUps
{
    public RecordHeroLevelUp[] records;
}

[System.Serializable]
public struct RecordHeroLevelUp
{
    public int id;
    public FightInfo fightInfo;
    public long price;
}