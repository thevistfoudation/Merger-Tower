using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.UI;
using LTAUnityBase.Base.DesignPattern;
public class BtnGame : MonoBehaviour
{
    [SerializeField]
    ButtonController BtnMerger,BtnSpamTowwer,BtnCoin;
    // Start is called before the first frame update
    void Start()
    {
        BtnSpamTowwer.OnClick((ButtonController btn) =>
        {
            Spawns.Instance.Creat();
            Debug.Log("tao ra tru");
        });

        BtnMerger.OnClick((ButtonController btn) =>
        {
           
        });
        BtnCoin.OnClick((ButtonController btn) =>
        {
            
        });
    }

}
