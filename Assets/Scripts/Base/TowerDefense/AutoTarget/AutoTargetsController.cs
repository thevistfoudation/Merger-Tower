using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTargetsController : MonoBehaviour
{
    IGetTargetsAuto[] getTargetsAutos;

    IFighter fighter;

    private void Start()
    {
        getTargetsAutos = GetComponents<IGetTargetsAuto>();
        fighter = GetComponent<IFighter>();
    }

    // Update is called once per frame
    void Update()
    {

        if (getTargetsAutos == null || getTargetsAutos.Length == 0 || fighter == null) return;

        foreach (IGetTargetsAuto getTargetAuto in getTargetsAutos)
        {
            List<EntityController> entities = EntityController.getTargets(fighter, getTargetAuto);

            if (entities != null && entities.Count > 0)
            {
                getTargetAuto.GetTarget(entities);
            }
        }
    }
}
