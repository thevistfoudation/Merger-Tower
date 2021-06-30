using System.Collections.Generic;

[System.Serializable]
public class EntityInfo
{
    public float armor = 0;

    public float magicResistance = 0;

    public float lifeSteal = 0;

    public float HP = 0;

    public float reflect = 0;

    public static EntityInfo operator +(EntityInfo a, EntityInfo b)
    {
        EntityInfo entity = new EntityInfo();
        entity.armor = a.armor + b.armor;
        entity.HP = a.HP + b.HP;
        entity.lifeSteal = a.lifeSteal + b.lifeSteal;
        entity.magicResistance = a.magicResistance + b.magicResistance;
        entity.reflect = a.reflect + b.reflect;
        return entity;
    }

    public static EntityInfo operator -(EntityInfo a, EntityInfo b)
    {
        EntityInfo entity = new EntityInfo();
        entity.armor = a.armor - b.armor;
        entity.HP = a.HP - b.HP;
        entity.lifeSteal = a.lifeSteal - b.lifeSteal;
        entity.magicResistance = a.magicResistance - b.magicResistance;
        entity.reflect = a.reflect - b.reflect;
        return entity;
    }

    public static EntityInfo operator *(EntityInfo a, EntityInfo b)
    {
        EntityInfo entity = new EntityInfo();
        entity.armor = a.armor * b.armor;
        entity.HP = a.HP * b.HP;
        entity.lifeSteal = a.lifeSteal * b.lifeSteal;
        entity.magicResistance = a.magicResistance * b.magicResistance;
        entity.reflect = a.reflect * b.reflect;
        return entity;
    }

    public static EntityInfo operator /(EntityInfo a, float b)
    {

        EntityInfo entity = new EntityInfo();
        if (b == 0) return entity;
        entity.armor = (float)a.armor / b;
        entity.HP = (float)a.HP / b;
        entity.lifeSteal = (float)a.lifeSteal / b;
        entity.magicResistance = (float)a.magicResistance / b;
        entity.reflect = (float)a.reflect / b;
        return entity;
    }
}

public class Target
{
    public static List<EntityController> GetEntities(List<Target> targets)
    {
        List<EntityController> entities = new List<EntityController>();

        foreach (Target target in targets)
        {
            entities.Add(target.entity);
        }
        return entities;
    }
    public float distance;
    public EntityController entity;
    public Target(float distance, EntityController entity)
    {
        this.distance = distance;
        this.entity = entity;
    }
}