using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = LTAUnityBase.Base.Character.CharacterController;
public class FearCharacter : CharacterController
{
    bool isEnd = false;

    Vector3 fearDirection;

    public Vector3 FearDirection
    {
        set
        {
            fearDirection = value;
            if (fearDirection != Vector3.zero && Main.CurrentState != CharacterState.Fear)
            {
                isEnd = false;
                Main.SetState(CharacterState.Fear);
            }
            else
            {
                isEnd = true;
                Main.SetState(CharacterState.Idle);
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        Main.AddCharaterState(CharacterState.Fear, this);
    }

    protected override void Update()
    {
        if (Main.CurrentState == CharacterState.Fear)
        {
            MoveController.Move(fearDirection);
        }
        else
        {
            base.Update();
        }
    }

    public override void SetState(CharacterState state)
    {
        if (state == CharacterState.Fear)
        {
            UpdateState(fearDirection);
            return;
        }
        base.SetState(state);
    }

    public override bool CheckCanChangeState(CharacterState state)
    {
        return base.CheckCanChangeState(state) && (Main.CurrentState != CharacterState.Fear || isEnd || state == CharacterState.Die);
    }
}
