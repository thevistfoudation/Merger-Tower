using UnityEngine;
namespace LTAUnityBase.Base.Character
{
    [DisallowMultipleComponent]
    public abstract class CharacterFighter : BaseCharaterController, ICharacterFighter
    {

        IFighter fighter;

        protected IFighter Fighter
        {
            get
            {
                if (fighter == null)
                    fighter = GetComponent<IFighter>();
                return fighter;
            }
        }

        private void Awake()
        {
            Main.AddCharaterState(CharacterState.Attack, this);
        }

        public void StartAttack(string AnimAttackName = "Attack")
        {
            Animator.Play(AnimAttackName,0);
        }

        public void EndAttack()
        {
            //Khong chac chan la no co anh huong performance hay ko!Neu anh huong sua thanh dem time bang Update
            StartCoroutine(Utils.IeDelayTimeToDo(Time.deltaTime, () =>
             {
                 HandleEndAttack();
             }));
        }

        protected virtual void HandleEndAttack()
        {
            Main.SetState(CharacterState.Idle);
        }

        public abstract void Attacked();

        public override bool CheckCanChangeState(CharacterState state)
        {
            return true;
        }
    }
}
