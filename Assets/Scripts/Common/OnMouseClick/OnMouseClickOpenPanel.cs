using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseClickOpenPanel : OnMouseClick
{
    public MonoBehaviour panel;


    protected override void OnMouseDown()
    {
        if (panel == null)
        {
            Debug.LogError("can not find panel on " + gameObject.name);
            return;
        }
        PanelRoot.Show(panel);
    }
}
