using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[UIPanelPrefabAttr("Panel/PanelBuyHeroes", "Popup")]

public class UIPanelHeroBuyer : MonoBehaviour
{
    [SerializeField]
    GameObject itemScrollPrefab;
    [SerializeField]
    Transform itemScrollParent;
    private UnityAction<string, int> actionDeployHero;
    private UnityAction actionRemoveHero;
    private string heroDeployedName = string.Empty;
    private List<ItemHeroInScroll> lstItems = new List<ItemHeroInScroll>();

    private void Awake()
    {
        PanelRoot.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        RecordHeroInfo[] infos = DataController.Instance.heroesVO.GetDataHeroesByName<RecordHeroInfo>("data_hero_infos");
        RecordHeroLevelUp[] levelups = new RecordHeroLevelUp[infos.Length];
        int length = infos.Length;
        for (int i = 0; i < length; i++)
        {
            RecordHeroLevelUp[] levelup = DataController.Instance.heroesVO.GetDataHeroesByName<RecordHeroLevelUp>(infos[i].name);
            CreateItem(infos[i], levelup);
        }
    }

    public void SetAction(UnityAction<string, int> action_deploy, UnityAction action_remove, string hero_deployed_name, int current_level_index)
    {
        actionDeployHero = action_deploy;
        actionRemoveHero = action_remove;
        if (!heroDeployedName.Equals(hero_deployed_name))
        {
            int count = lstItems.Count;
            for (int i = 0; i < count; i++)
            {
                if (lstItems[i] == null) continue;
                ItemHeroInScroll item = lstItems[i];
                item.SetState(false);
                item.SetPrice(0);
                if (lstItems[i].GetHeroName().Equals(hero_deployed_name))
                {
                    item.SetState(true);
                    item.SetPrice(current_level_index);
                }
            }
            heroDeployedName = hero_deployed_name;
        }
    }

    private void CreateItem(RecordHeroInfo info, RecordHeroLevelUp[] levelup)
    {
        Transform itemTf = Instantiate(itemScrollPrefab).transform;
        itemTf.SetParent(itemScrollParent);
        itemTf.localPosition = Vector3.zero;
        itemTf.localScale = Vector3.one;
        itemTf.gameObject.SetActive(true);
        ItemHeroInScroll item = itemTf.GetComponent<ItemHeroInScroll>();
        item.SetInfo(info, levelup);
        item.actionDeploy = (hero_deployed_name, current_level_index) => { actionDeployHero?.Invoke(hero_deployed_name, current_level_index); HidePanel(); };
        item.actionRemove = () => { actionRemoveHero?.Invoke(); HidePanel(); };
        lstItems.Add(item);
    }

    public void HidePanel()
    {
        PanelRoot.Hide(this);
    }

    private void OnDestroy()
    {
        actionDeployHero = null;
        actionRemoveHero = null;
        heroDeployedName = string.Empty;
        lstItems.Clear();
    }
}
