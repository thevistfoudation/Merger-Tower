using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public abstract class MergeController : MonoBehaviour
{
    static List<MergeController> merges = new List<MergeController>();

    public static MergeController GetMergeObject(MergeController checkedObject)
    {
        MergeController result = null;
        float minDistance = 100000f;
        foreach (MergeController merge in merges)
        {
            if (merge == checkedObject) continue;
            float distance = Vector3.Distance(merge.transform.position, checkedObject.transform.position);
            if (checkedObject.CheckMerge(merge) && (distance <=  minDistance || result == null))
            {
                Debug.Log("MERGER");
                result = merge;
                minDistance = distance;
            }
        }
        return result;
    }
  
    public abstract bool CheckMerge(MergeController merge);

    protected virtual void Awake()
    {
        merges.Add(this);
    }

    private void OnMouseDrag()
    {
        
    }

    protected virtual void OnMouseUp()
    {
        MergeController merge = GetMergeObject(this);
        if (merge != null) Merge(merge);
    }

    protected abstract void Merge(MergeController merge);

    private void OnDestroy()
    {
        merges.Remove(this);
    }
}

public class Merge : SingletonMonoBehaviour<MergeController>
{

}