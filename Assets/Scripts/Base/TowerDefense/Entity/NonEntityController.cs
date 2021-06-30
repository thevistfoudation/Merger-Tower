using UnityEngine;
using TMPro;

public class NonEntityController : MonoBehaviour
{
    [SerializeField]
    TextMeshPro txtLevel;

    IOnUpLevel onUpLevel;

    int level;

    IOnUpLevel OnUpLevel
    {
        get
        {
            if (onUpLevel == null)
                onUpLevel = GetComponent<IOnUpLevel>();
            return onUpLevel;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
    }

    public void SetLevel(int level)
    {
        this.level = level;
        if (txtLevel != null)
            txtLevel.text = this.level.ToString();
        
        if (OnUpLevel != null)
        {
            OnUpLevel.OnUpLevel(level);
        }
    }
  

    public void UpLevel()
    {
        SetLevel(level + 1);
    }
}
