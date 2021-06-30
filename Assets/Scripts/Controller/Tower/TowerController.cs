using UnityEngine;

public abstract class TowerController<T> : MonoBehaviour,IOnUpLevel where T : FightInfo
{
    [SerializeField]
    protected NonEntityController nonEntityController;

    public abstract NonEntityController NonEntityController
    {
        get;
    }

    public void OnUpLevel(int level)
    {
        T towerInfo = DataController.Instance.towersVO.GetData<T>(NonEntityController.name, level);
        NonEntityController.GetComponent<FightController<T>>().Info = towerInfo;
    }
}
