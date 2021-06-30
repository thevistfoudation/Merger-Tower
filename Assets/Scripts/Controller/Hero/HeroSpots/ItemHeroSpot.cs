using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeroSpot : OnMouseClick
{
    public bool isLock = false;
    private GameObject hero;
    private string heroDeployedName = string.Empty;
    private int heroCurrentLevelIndex = 0;

    private void Start()
    {
        SetLockSpot(isLock);
    }

    public void SetLockSpot(bool state)
    {
        GameObject spotLock = transform.GetChild(0).gameObject;
        GameObject spotUnLock = transform.GetChild(1).gameObject;
        if (spotLock == null || spotUnLock == null) return;
        if (spotLock != null && spotLock.name.Contains("unlock"))
        {
            GameObject temp = spotLock;
            spotLock = spotUnLock;
            spotUnLock = temp;
        }
        spotLock.SetActive(state);
        spotUnLock.SetActive(!state);
        isLock = state;
    }

    public void DeployHero(string hero_deployed_name, int current_level_index)
    {
        RemoveHero();
        heroDeployedName = hero_deployed_name;
        heroCurrentLevelIndex = current_level_index;
        GameObject heroPrefab = Resources.Load<GameObject>("Heroes/" + heroDeployedName);
        if (heroPrefab == null)
        {
            Debug.LogError("can not find hero " + heroDeployedName);
            return;
        }
        hero = Instantiate(heroPrefab);
        Transform tf = hero.transform;
        tf.SetParent(transform);
        tf.localPosition = Vector3.zero;
        tf.localScale = new Vector3(4, 4, 1);
        heroPrefab.SetActive(true);
    }

    public void RemoveHero()
    {
        if (hero != null)
        {
            Destroy(hero);
            heroDeployedName = string.Empty;
            heroCurrentLevelIndex = 0;
        }
    }



    protected override void OnMouseDown()
    {
        if (isLock) return;
        PanelRoot.Show<UIPanelHeroBuyer>().SetAction((hero_deployed_name, hero_current_level_index) => { DeployHero(hero_deployed_name, hero_current_level_index); }, () => { RemoveHero(); }, heroDeployedName, heroCurrentLevelIndex) ;
    }

    private void OnDestroy()
    {
        RemoveHero();
        hero = null;
        heroDeployedName = null;
        heroCurrentLevelIndex = 0;
    }
}
