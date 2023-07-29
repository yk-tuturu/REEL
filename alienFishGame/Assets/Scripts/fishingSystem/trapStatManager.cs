using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStatManager : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    public int maxCapacity;

    public int rodLevel = 1;
    public int trapLevel = 1;
    public int baitLevel = 1;

    public float uncommonProb;
    public float rareProb;

    public TextAsset levelJson;
    public Level[] levels;

    public static TrapStatManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }  

        Levels levelList = JsonUtility.FromJson<Levels>(levelJson.text);
        levels = levelList.levels;

        SetTrapLevel(1);
        SetBaitLevel(1);
    }

    public void SetTrapLevel(int level)
    {
        trapLevel = level;
        var levelStats = levels[level - 1];
        minTime = levelStats.minTime;
        maxTime = levelStats.maxTime;
        maxCapacity = levelStats.maxCapacity;
    }

    public void SetBaitLevel(int level)
    {
        baitLevel = level;
        var levelStats = levels[level - 1];
        uncommonProb = levelStats.uncommonProb;
        rareProb = levelStats.rareProb;
    }

    public void LoadData(SaveData saveData)
    {
        foreach(var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == "trap")
            {
                trapLevel = upgrade.currentLevel;
                SetTrapLevel(trapLevel);
                Debug.Log(trapLevel.ToString());
            }

            if (upgrade.type == "bait")
            {
                baitLevel = upgrade.currentLevel;
                SetBaitLevel(baitLevel);
            }
        }
    }
}
