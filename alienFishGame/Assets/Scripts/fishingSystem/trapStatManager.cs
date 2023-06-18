using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStatManager : MonoBehaviour
{
    public float minTime = 5.5f;
    public float maxTime = 11f;
    public int maxCapacity = 3;

    public int rodLevel = 1;
    public int trapLevel = 1;
    public int baitLevel = 1;

    public float uncommonProb = 0.05f;
    public float rareProb = 0f;

    public static TrapStatManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }  

        DontDestroyOnLoad(this.gameObject);
    }

    // to whoever looks at this section of the code, i am sorry for breaking all fundamental laws of programming
    public void UpgradeTrap()
    {
        trapLevel += 1; 
        
        if (trapLevel == 2)
        {
            minTime = 3.5f;
            maxTime = 7f;
            maxCapacity = 6;
        }
        else if (trapLevel == 3)
        {
            minTime = 2.5f;
            maxTime = 5f;
            maxCapacity = 9;
        }
    }

    public void UpgradeBait()
    {
        baitLevel += 1;
        if (baitLevel == 2)
        {
            rareProb = 0.02f;
            uncommonProb = 0.1f;
        }
        if (baitLevel == 3)
        {
            rareProb = 0.05f;
            uncommonProb = 0.2f;
        }
    }

    public void LoadData(SaveData saveData)
    {
        foreach(var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == "trap")
            {
                trapLevel = upgrade.currentLevel - 1;
            }

            if (upgrade.type == "bait")
            {
                baitLevel = upgrade.currentLevel - 1;
            }
        }

        UpgradeTrap();
        UpgradeBait();
    }
}
