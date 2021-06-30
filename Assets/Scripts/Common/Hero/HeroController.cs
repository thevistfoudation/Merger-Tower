using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//public class HeroInfo
//{
//    public FightInfo fightInfo;

//}

public class HeroController : AcheroController
{
    private void Start()
    {
        NonEntityController.SetLevel(1);
    }
    //public bool checkFight(IFighter fighter, EntityController entity)
    //{
    //    return Mathf.Abs(entity.transform.position.y - transform.position.y)<= 0.2;
    //}

    //public void OnUpLevel(int level)
    //{
    //    throw new System.NotImplementedException();
    //}

}
