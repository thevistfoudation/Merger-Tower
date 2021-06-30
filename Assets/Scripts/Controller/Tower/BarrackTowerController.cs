using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BarrackInfo
{
    public int numElement;
    public int level;
    public string type;
    public float timeSpawn;
}
[RequireComponent(typeof(NonEntityController))]
public class BarrackTowerController : MonoBehaviour,IOnUpLevel
{
    [SerializeField]
    BarrackInfo info;

    NonEntityController nonEntityController;

    [SerializeField]
    Transform tran_Pos;

    List<EntityController> entities = new List<EntityController>();

    [SerializeField]
    EntityController prefabElement;

    public NonEntityController NonEntityController
    {
        get
        {
            if (nonEntityController == null)
            {
                nonEntityController = GetComponent<NonEntityController>();
            }
            return nonEntityController;
        }
    }

    private void Start()
    {
        NonEntityController.SetLevel(1);
    }

    void SpawnElement()
    {
        Vector3 pos = new Vector3(
                Random.Range(tran_Pos.position.x - 1,tran_Pos.position.x + 1),
                tran_Pos.position.y,
                0
                );
        EntityController entity = Creater.Instance.creatEntityPlayer<EntityController>(prefabElement, pos,info.level);
        entities.Add(entity);
    }

    float timeCount = 0;

    private void Update()
    {
        if (info == null) return;
        int numSpawn = info.numElement - entities.Count;
        if (numSpawn <= 0) return;
        if (timeCount < info.timeSpawn)
        {
            timeCount += Time.deltaTime;
            return;
        }
        SpawnElement();
        timeCount = 0;
    }

    void OnElementDie(EntityController entity)
    {
        entities.Remove(entity);
        timeCount = 0;
    }
    public void OnUpLevel(int level)
    {
        info = DataController.Instance.towersVO.GetData<BarrackInfo>(name, level);
        prefabElement = Resources.Load<EntityController>("Fighter/" + info.type);
    }
}
