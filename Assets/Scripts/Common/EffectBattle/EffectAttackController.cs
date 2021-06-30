using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class EffectAttackController : MonoBehaviour,IAttacker
{
    public BattleEffectInfo[] battleEffects;

    public void MoreAttack(GameObject target, RealDamage damage)
    {
        EffectBattleController[] effectBattles = target.GetComponents<EffectBattleController>();
        if (battleEffects == null)
        {
            Destroy(this);
            return;
        }
        foreach(BattleEffectInfo effectInfo in battleEffects)
        {
            float isShow = Random.Range(1f, 100f);
            Debug.Log("effectInfo " + isShow);
            if (isShow > effectInfo.percentEffect) continue;

            bool isAdded = false;
            foreach(EffectBattleController effect in effectBattles)
            {
                if (effect.EffectName == effectInfo.nameEffect)
                {
                    effect.ResetTime();
                    isAdded = true;
                    break;
                }
            }
            if (isAdded) continue;
            EffectBattleController effectBattle = target.AddComponent<EffectBattleController>();
            effectBattle.Info = effectInfo;
        }
    }
}
