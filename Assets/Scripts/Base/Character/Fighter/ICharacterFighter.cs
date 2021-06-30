namespace LTAUnityBase.Base.Character
{
    public interface ICharacterFighter
    {
        void StartAttack(string AnimAttackName = "Attack");

        void Attacked();

        void EndAttack();
    }
}
