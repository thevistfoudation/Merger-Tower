using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffectActive<T> : MonoBehaviour,IEffectBattle
{
    T info;

    public virtual object Info {
        set {
            info = (T) value;
        }
    }

    protected T EffectInfo
    {
        get
        {
            return info;
        }
    }

    public abstract void ActiveEffect();
}
