using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemHeroInScroll : OnMouseClick
{
    [SerializeField]
    RawImage icon;
    [SerializeField]
    Text txState, txPrice;
    private RecordHeroInfo recordInfo;
    private RecordHeroLevelUp[] recordLevelUp;
    public UnityAction<string, int> actionDeploy;
    public UnityAction actionRemove;
    public UnityAction actionLevelup;
    private bool deployed = false;
    private int currentLevel = -1;

    public void SetInfo(RecordHeroInfo info, RecordHeroLevelUp[] level_up)
    {
        this.recordInfo = info;
        this.recordLevelUp = level_up;
        icon.texture = Resources.Load<Texture2D>(info.icon);
        currentLevel = 0;
        SetPrice(currentLevel);
    }

    public void SetState(bool deployed)
    {
        this.deployed = deployed;
        if (txState != null)
            txState.text = deployed ? "Remove" : "Deploy";
    }
    public void SetPrice(int index)
    {
        if (index <= 0)
            index = 0;
        int nextPrice = index;
        if (deployed)
        {
            nextPrice = index + 1;
            if (txPrice != null)
            {
                if (nextPrice >= recordLevelUp.Length)
                    txPrice.text = "Max";
                else
                    txPrice.text = "Level up $" + recordLevelUp[nextPrice].price;
            }
        }
        else
        {
            if (txPrice != null)
                txPrice.text = "Level up $" + recordLevelUp[nextPrice].price;
        }
        currentLevel = index;
    }

    public void DeployOrRemove()
    {
        if (!deployed)
        {
            actionDeploy?.Invoke(recordInfo.name, currentLevel);
        }
        else
        {
            actionRemove?.Invoke();
        }
    }

    public void LevelUp()
    {
        int index = currentLevel + 1;
        if (index >= recordLevelUp.Length) return;
        actionLevelup?.Invoke();
        SetPrice(index);
    }

    public string GetHeroName()
    {
        return recordInfo.name;
    }


    protected override void OnMouseDown()
    {
    }

    private void OnDestroy()
    {
        actionDeploy = null;
        actionRemove = null;
        actionLevelup = null;
    }
}
