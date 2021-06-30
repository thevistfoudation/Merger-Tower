using UnityEngine;
namespace LTAUnityBase.Base.Character
{
    [RequireComponent(typeof(MoveController))]
    public abstract class CharacterController : CharacterCanDieController
    {

        IOnCharacterMove onCharacterMove;

        MoveController moveController;

        protected MoveController MoveController
        {
            get
            {
                if (moveController == null)
                {
                    moveController = GetComponent<MoveController>();
                }
                return moveController;
            }
        }

        enum MoveState
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        MoveState moveState = MoveState.None;

        protected Vector3 direction = Vector3.zero;

        public virtual Vector3 Direction
        {
            set
            {
                direction = value;
                UpdateState(direction);
            }
        }

        protected override void Start()
        {
            base.Start();
            Main.AddCharaterState(CharacterState.Move, this);
            onCharacterMove = GetComponent<IOnCharacterMove>();
        }

        protected override void Update()
        {
            if (Main.CurrentState == CharacterState.Move)
            {
                MoveController.Move(direction);
                if (onCharacterMove != null)
                {
                    onCharacterMove.OnMove(direction);
                }
            }
            else
            {
                base.Update();
            }
        }

        protected override void ChangeAnimation(CharacterState state)
        {
            if (state != CharacterState.Move)
                Direction = Vector3.zero;
            base.ChangeAnimation(state);

        }

        protected void UpdateState(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                moveState = MoveState.None;
                return;
            }
            
            if (direction.y > 0)
                moveState = MoveState.Up;
            else if (direction.y < 0)
                moveState = MoveState.Down;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) || direction.y == 0)
            {
                if (direction.x > 0)
                    moveState = MoveState.Right;
                else
                    moveState = MoveState.Left;
            }

            switch (moveState)
            {
                case MoveState.Up:
                    Animator.Play("Move Up");
                    return;
                case MoveState.Down:
                    Animator.Play("Move Down");
                    return;
                case MoveState.Left:
                    Animator.Play("Move Left");
                    return;
                case MoveState.Right:
                    Animator.Play("Move Right");
                    return;
            }
        }
    }
}
