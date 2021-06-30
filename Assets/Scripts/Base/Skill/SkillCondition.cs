using UnityEngine;

public class ConditionInfo
{
    public SkillCharInfo skillCharInfo;
}

[RequireComponent(typeof(SkillCharacter))]
public class SkillCondition<T> : MonoBehaviour where T : ConditionInfo
{
    T info;

    public virtual T Info
    {
        set
        {
            info = value;
        }

        protected get
        {
            return info;
        }
    }

    SkillCharacter skillCharacter;

    SkillCharacter SkillCharacter
    {
        get
        {
            if (skillCharacter == null)
                skillCharacter = GetComponent<SkillCharacter>();
            return skillCharacter;
        }
    }

    protected void ShowSkill()
    {
        SkillCharacter.SkillCharInfo = info.skillCharInfo;
    }
}
