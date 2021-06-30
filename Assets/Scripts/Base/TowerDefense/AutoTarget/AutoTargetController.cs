using UnityEngine;
[DisallowMultipleComponent]
public class AutoTargetController : MonoBehaviour
{
    IGetTargetAuto[] getTargetAutos;

    IFighter fighter;

    private void Start()
    {
        getTargetAutos = GetComponents<IGetTargetAuto>();
        fighter = GetComponent<IFighter>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (getTargetAutos == null || getTargetAutos.Length == 0 || fighter == null) return;

        foreach (IGetTargetAuto getTargetAuto in getTargetAutos)
        {
            EntityController entity = EntityController.getTarget(fighter, getTargetAuto);
            
            if (entity != null)
            {
                getTargetAuto.GetTarget(entity);
            }
        }
    }
}
