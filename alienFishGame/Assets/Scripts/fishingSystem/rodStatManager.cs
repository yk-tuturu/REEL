using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodStatManager : MonoBehaviour
{
    public float minTime = 5.5f;
    public float maxTime = 11f;
    public int maxCapacity = 3;

    public int rodLevel = 1;
    public int trapLevel = 1;
    public int baitLevel = 1;

    public float uncommonProb = 0.05f;
    public float rareProb = 0f;

    // maybe turn this into a struct in the future. contains index for level and the dict
    // public Dictionary<string, float> level1Stats = new Dictionary<string, float>() {"minTime" = 4f, "maxTime" = 10f, "uncommonProb" = 0.05f, "rareProb" = 0f};
    // public Dictionary<string, float> level2Stats = new Dictionary<string, float>() {"minTime" = 2.5f, "maxTime" = 6.5f, "uncommonProb" = 0.1f, "rareProb" = 0.02f};
    // public Dictionary<string, float> level3Stats = new Dictionary<string, float>() {"minTime" = 1f, "maxTime" = 3f, "uncommonProb" = 0.2f, "rareProb" = 0.05f};

    public static RodStatManager instance;

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
    }

    // to whoever looks at this section of the code, i am sorry for breaking all fundamental laws of programming
    // okay we definitely need to fix this up somehow
    public void UpgradeRod()
    {
        rodLevel += 1; 
        if (rodLevel == 2)
        {
            minTime = 3.5f;
            maxTime = 7f;
            maxCapacity = 6;
        }
        else if (rodLevel == 3)
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
            if (upgrade.type == "rod")
            {
                rodLevel = upgrade.currentLevel - 1;
            }

            if (upgrade.type == "bait")
            {
                baitLevel = upgrade.currentLevel - 1;
            }
        }

        UpgradeRod();
        UpgradeBait();
    }
}
