using UnityEngine;

public class SkillCharInfo
{
    public string StartSkillAnim;
    public string EndSkillAnim;
    public SkillEffectInfo skillInfo;
}

public class SkillCharacter : BaseCharaterController
{
    bool isEnding = false;

    public override bool CheckCanChangeState(CharacterState state)
    {
        return isEnding;
    }
    SkillCharInfo skillCharInfo;

    private void Awake()
    {
        Main.AddCharaterState(CharacterState.Skill,this);
    }

    public override void SetState(CharacterState state)
    {
        Animator.Play(skillCharInfo.StartSkillAnim);
    }

    public SkillCharInfo SkillCharInfo
    {
        set
        {
            skillCharInfo = value;
            Main.SetState(CharacterState.Skill);
        }
    }

    public void ActiveSkill()
    {
        SkillEffectController skillEffectController = gameObject.AddComponent<SkillEffectController>();
        skillEffectController.EndSkill = EndAnimSkill;
        skillEffectController.Info = skillCharInfo.skillInfo;
    }

    public void EndAnimSkill()
    {
        Animator.Play(skillCharInfo.EndSkillAnim);
    }

    public void EndSkill() 
    {
        isEnding = true;
        Main.SetState(CharacterState.Idle);
    }
}
