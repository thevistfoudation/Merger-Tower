using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTowerController : MonoBehaviour
{
    NonEntityController nonEntityController;

    public  NonEntityController NonEntityController
    {
        get
        {
            if (nonEntityController == null)
            {
                nonEntityController = GetComponentInChildren<NonEntityController>();
            }
            return nonEntityController;
        }
    }

    private void Start()
    {
        NonEntityController.SetLevel(1);
    }
}
