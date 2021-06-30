using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;

public class SpawnRandom : MonoBehaviour
{
    public GameObject[] poinSpawn;

    public GameObject[] towers;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Creat()
    {
        int posCrTower = Random.Range(0, towers.Length);
        int posRandom = Random.Range(0, poinSpawn.Length);
        GameObject creatObject = Instantiate(towers[posCrTower], poinSpawn[posRandom].transform.position, Quaternion.identity) as GameObject;
    }
}
public class Spawns : SingletonMonoBehaviour<SpawnRandom>
{

}
