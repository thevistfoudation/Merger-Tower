using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotsController : MonoBehaviour
{
    static List<SlotsController> Slots = new List<SlotsController>();

    public static SlotsController GetMergeObject(SlotsController checkedObject)
    {
        SlotsController result = null;
        float minDistance = 0.56f;
        foreach (SlotsController Slot in Slots)
        {
            if (Slot == checkedObject) continue;
            float distance = Vector3.Distance(Slot.transform.position, checkedObject.transform.position);
            if (checkedObject.CheckSlot(Slot) && (distance <= minDistance || result == null))
            {
                result = Slot;
                minDistance = distance;
            }
        }
        return result;
    }
    public abstract bool CheckSlot(SlotsController Slot);

    private void Awake()
    {
        Slots.Add(this);
    }

    private void OnMouseDrag()
    {
        //Debug.Log(this.gameObject.transform.position)
    }
    private void OnMouseUp()
    {
        SlotsController Slot = GetMergeObject(this);
        if (Slot != null) MergeSlot(Slot);
    }
    protected abstract void MergeSlot(SlotsController Slot);

    private void OnDestroy()
    {
        Slots.Remove(this);
    }
}