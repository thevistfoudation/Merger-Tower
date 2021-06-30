using LTAUnityBase.Base.Character;
using UnityEngine;
[RequireComponent(typeof(NonEntityController), typeof(NPCFighterController), typeof(ShootController))]
public class AcheroController : MonoBehaviour,IOnUpLevel
{
    NonEntityController nonEntityController;
    public NonEntityController NonEntityController
    {
        get
        {
            if (nonEntityController == null)
                nonEntityController = GetComponent<NonEntityController>();
            return nonEntityController;
        }
    }

    ShootController shootController;

    ShootController ShootController
    {
        get
        {
            if (shootController == null)
            {
                shootController = GetComponent<ShootController>();
            }
            return shootController;
        }
    }

    public virtual void OnUpLevel(int level)
    {
        if (DataController.Instance.fightersVO == null) return;
        ShootInfo shootInfo = DataController.Instance.fightersVO.GetData<ShootInfo>(name,level);
        Debug.Log(name + " " + shootInfo == null);
        ShootController.Info = shootInfo;
    }
}
