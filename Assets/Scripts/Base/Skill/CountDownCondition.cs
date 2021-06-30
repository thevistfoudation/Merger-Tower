using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownInfo : ConditionInfo
{
    public float timeCountDown;
}

public class CountDownCondition : SkillCondition<CountDownInfo>
{

    public float countDown = 0;

    public override CountDownInfo Info
    {
        protected get => base.Info;
        set
        {
            base.Info = value;
            ShowSkill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown < Info.timeCountDown)
        {
            countDown += Time.deltaTime;
            return;
        }
        countDown = 0;
        ShowSkill();
    }
}
