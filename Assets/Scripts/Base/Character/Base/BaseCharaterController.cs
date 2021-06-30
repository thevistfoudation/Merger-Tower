using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.Controller;

[RequireComponent(typeof(CharateterStateController),typeof(Animator))]
public abstract class BaseCharaterController : BaseMainComponent<CharateterStateController>,ICharacterState
{
    Animator animator;

    protected Animator Animator
    {
        get
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            return animator;
        }
    }

    public abstract bool CheckCanChangeState(CharacterState state);
    public abstract void SetState(CharacterState state);
}
