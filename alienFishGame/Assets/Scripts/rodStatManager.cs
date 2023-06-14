using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodStatManager : MonoBehaviour
{
    public float minTime = 4f;
    public float maxTime = 10f;
    public int maxCapacity = 3;

    public int rodLevel = 1;
    public int trapLevel = 1;
    public int baitLevel = 1;

    public float uncommonProb = 0.05f;
    public float rareProb = 0f;

    public static RodStatManager instance;

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
    }

    // to whoever looks at this section of the code, i am sorry for breaking all fundamental laws of programming
    public void UpgradeRod()
    {
        rodLevel += 1; 
        maxCapacity += 3;
        if (rodLevel == 2)
        {
            minTime = 2.5f;
            maxTime = 6f;
        }
        else if (rodLevel == 3)
        {
            minTime = 1f;
            maxTime = 3f;
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
}
