using LTAUnityBase.Base.Controller;
using UnityEngine;

namespace LTAUnityBase.Base.Character
{
    [DisallowMultipleComponent]
    public class CharacterIdleController : BaseCharaterController
    {
        IOnCharacterIdle onCharacterIdle;

        protected virtual void Start()
        {
            Main.AddCharaterState(CharacterState.Idle, this);
            onCharacterIdle = GetComponent<IOnCharacterIdle>();
        }

        protected virtual void Update()
        {
            if (Main.CurrentState == CharacterState.Idle && onCharacterIdle != null)
            {
                onCharacterIdle.Idle();
            }

        }

        public override void SetState(CharacterState state)
        {
            ChangeAnimation(state);
        }

        protected virtual void ChangeAnimation(CharacterState state)
        {
            if (state == CharacterState.Idle)
            {
                Animator.Play("Idle");
            }
        }

        public override bool CheckCanChangeState(CharacterState state)
        {
            return true;
        }
    }
}
