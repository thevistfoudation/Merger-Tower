using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEffect : MonoBehaviour, IEffectBattle
{
    object info;

    public object Info { set
        {
            info = value;
        }
    }

    public void ActiveEffect()
    {
        FearCharacter fearCharacter = GetComponent<FearCharacter>();
        if (fearCharacter == null)
        {
            Destroy(this);
            return;
        }
        fearCharacter.FearDirection = Vector3.right;
    }

    private void OnDestroy()
    {
        FearCharacter fearCharacter = GetComponent<FearCharacter>();
        if (fearCharacter != null)
        {
            fearCharacter.FearDirection = Vector3.zero;
        }
        
    }
}
