using UnityEngine;
[System.Serializable]
public struct WarriorInfo
{
    public FightInfo fightInfo;
    public NPCCharaterInfo npcCharaterInfo;
    public EntityInfo entityInfo;
}

[RequireComponent(typeof(MeleeController))]
public class WarriorController : MonoBehaviour,IOnUpLevel
{
    MeleeController meleeController;

    MeleeController MeleeController
    {
        get
        {
            if (meleeController == null)
            {
                meleeController = GetComponent<MeleeController>();
            }
            return meleeController;
        }
    }

    NPCCharaterController npcCharaterController;

    NPCCharaterController NPCCharaterController
    {
        get
        {
            if (npcCharaterController == null)
            {
                npcCharaterController = GetComponent<NPCCharaterController>();
            }
            else
            {
                Debug.LogError("Entity Must Have NPCCharaterController");
            }
            return npcCharaterController;
        }
    }
    public void OnUpLevel(int level)
    {
        WarriorInfo warriorInfo = DataController.Instance.fightersVO.GetData<WarriorInfo>(name,level);
        MeleeController.Info = warriorInfo.fightInfo;
        MeleeController.Entity.EntityInfo = warriorInfo.entityInfo;
        NPCCharaterController.NPCCharaterInfo = warriorInfo.npcCharaterInfo;
        if (warriorInfo.fightInfo.battleEffects != null)
        {
            gameObject.AddComponent<EffectAttackController>().battleEffects = warriorInfo.fightInfo.battleEffects;
        }

    }

    private void Start()
    {
        NonEntityController nonEntityController = GetComponent<NonEntityController>();
        nonEntityController.SetLevel(1);
    }
}
