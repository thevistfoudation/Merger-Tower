using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySlotController : SlotsController
{
    public override bool CheckSlot(SlotsController Slot)
    {

        bool result = Slot.GetType() == this.GetType();
        result = result && (Slot.tag == this.gameObject.tag);
        return result;

    }

    protected override void MergeSlot(SlotsController Slot)
    {
        this.gameObject.transform.position = Slot.transform.position;
    }
}
