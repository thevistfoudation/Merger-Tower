using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBarController : CoinBarController
{
    protected override float Coin => GlobalVal.userInfo.Gold;
}
