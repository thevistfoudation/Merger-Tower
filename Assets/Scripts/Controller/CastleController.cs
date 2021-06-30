using UnityEngine;
using LTAUnityBase.Base.DesignPattern;

[DisallowMultipleComponent]
[RequireComponent(typeof(EntityController))]
public class CastleController : MonoBehaviour, IOnDie, IOnUpLevel
{
    public void OnDie()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpLevel(int level)
    {
        throw new System.NotImplementedException();
    }
}
public class Castle : SingletonMonoBehaviour<CastleController>
{

}
