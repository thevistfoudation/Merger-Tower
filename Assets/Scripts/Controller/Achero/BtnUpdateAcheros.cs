using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.UI;
using UnityEngine.EventSystems;

public class BtnUpdateAcheros : ButtonController
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Acheros.Instance.NonEntityController.UpLevel();
    }
}
