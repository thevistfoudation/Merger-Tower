using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;

public class CheckMergeController : MergeController
{

    public override bool CheckMerge(MergeController merge)
    {
        Debug.Log("checker1");
        Debug.Log(merge.name + this.gameObject.name);
        bool result = merge.GetType() == this.GetType();
        result = result && (merge.name == this.gameObject.name);
        result = result && (merge.GetComponent<NonEntityController>().Level ==
            this.gameObject.GetComponent<NonEntityController>().Level);
        return result;
    }

    protected override void Merge(MergeController merge)
    {
        this.gameObject.transform.position += merge.transform.position;
        merge.GetComponent<NonEntityController>().UpLevel();
        Debug.Log("Lv:" +merge.GetComponent<NonEntityController>().Level);
        Debug.Log("destroy");
        Destroy(this.gameObject);
        //LeanTween.delayedCall(0.5f, () =>
        //{
           
        //});
    }
}
public class CheckMer : SingletonMonoBehaviour<CheckMergeController>
{

}