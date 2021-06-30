using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBarController : CoinBarController
{
    protected override float Coin => GlobalVal.userInfo.Gem;
}
