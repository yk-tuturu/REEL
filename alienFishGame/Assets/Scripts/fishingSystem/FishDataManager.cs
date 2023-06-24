using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishDataManager : MonoBehaviour
{
    public static FishDataManager instance;

    public TextAsset jsonFile;
    public Fishes fishList;
    public Fish[] fishes;
    public int fishTypeCount = 5;
    public float money;
    public float salesMultiplier = 1.7f;
    public int allFishCaught = 0;
    public int rareCaught = 0;
    public bool bossAvailable = false;

    public int nextMilestone = 50;

    public UnityEvent unlockBoss;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } 

        Fishes fishList = JsonUtility.FromJson<Fishes>(jsonFile.text);

        fishes = fishList.fishes;
        fishTypeCount = fishes.Length;
    }
 
    void Start()
    {
        
    }

    void Update()
    {
        if (rareCaught >= 1 && allFishCaught >= 300 && !bossAvailable)
        {
            unlockBoss.Invoke();
            bossAvailable = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("i have been called!!");
            bgmScript.instance.SetParameter(7);
        }
    }

    // utility function, probably
    public Fish GetFish(int index)
    {
        return (fishes[index]);
    }

    public void ClaimFish(List<int> fishCaught)
    {
        foreach(var index in fishCaught)
        {
            fishes[index].totalCaught += 1;
            allFishCaught += 1;

            // checks if it's a first time catching a new rare fish
            if (fishes[index].rarity == 3 && fishes[index].totalCaught == 1)
            {
                rareCaught += 1;
            }
        }

        if (allFishCaught >= nextMilestone)
        {
            nextMilestone += 50;
            CheckMusicParameters();
        }
    }

    public void SellFish(int index, int number)
    {
        fishes[index].totalSold += number;
        money += fishes[index].price * number;
    }

    public void SpendMoney(int spentAmount)
    {
        money -= spentAmount;
    }

    public void UpgradeSales()
    {
        foreach (Fish fish in fishes)
        {
            fish.price = Mathf.Round(fish.price * salesMultiplier);
        }
    }

    public void LoadData(SaveData saveData)
    { 
        // refreshes fish data
        Fishes fishList = JsonUtility.FromJson<Fishes>(jsonFile.text);

        fishes = fishList.fishes;
        fishTypeCount = fishes.Length;

        // sales upgrade
        int salesLevel = 0;
        foreach (var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == "sales")
            {
                salesLevel = upgrade.currentLevel;
            }
        }

        var total = 0;
        var rareTotal = 0;
        for (int i = 0; i < fishTypeCount; i++)
        {
            fishes[i].totalCaught = saveData.totalCaught[i];
            fishes[i].totalSold = saveData.totalSold[i];
            fishes[i].price = Mathf.Round(fishes[i].price * (Mathf.Pow(salesMultiplier, salesLevel -1)));

            total += fishes[i].totalCaught;
            if (fishes[i].rarity == 3 && fishes[i].totalCaught > 0)
            {
                rareTotal += 1;
            }
        }

        allFishCaught = total;
        rareCaught = rareTotal;

        money = saveData.money;

        // set audio back to its correct param
        nextMilestone = (int)(Mathf.Floor(allFishCaught / 50) + 1)* 50;
        CheckMusicParameters();
    }

    public void CheckMusicParameters()
    {
        float value = allFishCaught / 50;
        float param = Mathf.Min(7, Mathf.Floor(value + 1));
        if (allFishCaught >= 250)
        {
            param = 7f;
        }

        float currentParam = bgmScript.instance.GetParameter();
        // if (currentParam > param)
        // {
        //     return;
        // }

        Debug.Log("fish param is " + param.ToString());
        bgmScript.instance.SetParameter(param);
    }
}
