using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotionLessEffectInfo
{
    public string animationName;
}

[DisallowMultipleComponent]
[RequireComponent(typeof(MotionLessCharacter))]
public class MotionLessEffect : BaseEffectActive<MotionLessEffectInfo>
{
    MotionLessCharacter motionLessCharacter;

    MotionLessCharacter MotionLessCharacter
    {
        get
        {
            if (motionLessCharacter == null)
            {
                motionLessCharacter = GetComponent<MotionLessCharacter>();
            }
            return motionLessCharacter;
        }
    }

    public override void ActiveEffect()
    {
        MotionLessCharacter.SetCharacterMotionLess(EffectInfo.animationName);
    }

    private void OnDestroy()
    {
        Destroy(MotionLessCharacter);
    }
}
