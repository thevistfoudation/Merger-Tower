using UnityEngine;

[DisallowMultipleComponent]
public class MotionLessCharacter : BaseCharaterController
{
    bool isEnd = false;

    private void Awake()
    {
        Main.AddCharaterState(CharacterState.MotionLess, this);
    }

    public override bool CheckCanChangeState(CharacterState state)
    {
        return state == CharacterState.Die || isEnd;
    }

    string m_AnimationName;

    public override void SetState(CharacterState state)
    {
        Animator.Play(m_AnimationName);
    }

    public void SetCharacterMotionLess(string animationName)
    {
        m_AnimationName = animationName;
        Main.SetState(CharacterState.MotionLess);
        
    }

    private void OnDestroy()
    {
        isEnd = true;
        Main.SetState(CharacterState.Idle);
    }
}