using UnityEngine;
namespace LTAUnityBase.Base.Character
{
    [RequireComponent(typeof(AutoDestroy))]
    public class CharacterCanDieController : CharacterIdleController,IAutoDestroy,IOnDie
    {
        bool isAutoDestroy = false;

        public bool IsAutoDestroy => isAutoDestroy;

        public float TimeCountDestroy => 1f;

        IOnCharacterDie onCharacterDie;

        protected override void Start()
        {
            base.Start();
            Main.AddCharaterState(CharacterState.Die, this);
            onCharacterDie = GetComponent<IOnCharacterDie>();
        }

        protected override void Update()
        {
            if (Main.CurrentState == CharacterState.Die && onCharacterDie != null)
                    onCharacterDie.Die();
            else
                base.Update();
        }

        protected override void ChangeAnimation(CharacterState state)
        {
            if (state == CharacterState.Die)
            {
                Animator.Play("Die");
            }
            else
            {
                base.ChangeAnimation(state);
            }
        }

        public void OnDie()
        {
            Main.SetState(CharacterState.Die);
        }

        public void EndAnimDie()
        {
            isAutoDestroy = true;
        }

        public override bool CheckCanChangeState(CharacterState state)
        {
            return base.CheckCanChangeState(state)&&Main.CurrentState != CharacterState.Die;
        }
    }
}
