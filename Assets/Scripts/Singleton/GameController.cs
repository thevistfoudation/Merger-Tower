using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        DataController.Instance.LoadLocalData();
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalVal.userInfo.Gold = 1000;
        
    }

    public void ShowHeroSlots()
    {
        Panel.Show<UIPanelHeroBuyer>();
    }
}
