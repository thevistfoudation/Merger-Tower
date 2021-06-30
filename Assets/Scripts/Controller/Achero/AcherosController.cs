using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
[RequireComponent(typeof(NonEntityController))]
public class AcherosController : MonoBehaviour, IOnUpLevel
{
    int level;

    AcheroController[] acheroes;

    AcheroController[] Acheroes
    {
        get
        {
            if (acheroes == null)
            {
                acheroes = GetComponentsInChildren<AcheroController>();
            }
            return acheroes;
        }
    }

    NonEntityController nonEntityController;
    public NonEntityController NonEntityController
    {
        get
        {
            if (nonEntityController == null)
                nonEntityController = GetComponent<NonEntityController>();
            return nonEntityController;
        }
    }

    public void Start()
    {
        AcheroController[] currentAcheros = Acheroes;
        foreach (AcheroController achero in currentAcheros)
        {
            achero.gameObject.SetActive(false);
        }
       NonEntityController.SetLevel(1);
    }

    public void OnUpLevel(int level)
    {
        AcheroController achero = Acheroes[(level-1)%Acheroes.Length];
        achero.NonEntityController.UpLevel();
        if (achero.NonEntityController.Level == 1)
        {
            achero.gameObject.SetActive(true);
        }
    }
}

public class Acheros : SingletonMonoBehaviour<AcherosController>
{

}
