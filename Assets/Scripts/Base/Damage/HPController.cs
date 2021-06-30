using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.Controller;
using System;
public class HPController : ProcessController
{
    public event Action die;

    public float HP
    {
        get
        {
            return currentValue;
        }
    }

    public void SetHP(float HP)
    {
        maxValue = HP;
        SetValue(maxValue);
    }

    public void TakeDamage(float damage)
    {
        EditValue(currentValue - damage);
    }

    public void Heal(float heal)
    {
        EditValue(currentValue + heal);
    }

    protected override void OnUpdate(float value)
    {
        transform.localScale = new Vector3((float)value / maxValue,transform.localScale.y, 1);
        if (value <= 0)
        {
            if (die != null)
            {
                die();
            }
        }
    }
}
