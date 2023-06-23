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

    public UnityEvent unlockBoss;

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

        Fishes fishList = JsonUtility.FromJson<Fishes>(jsonFile.text);

        fishes = fishList.fishes;
        fishTypeCount = fishes.Length;

        Debug.Log("fish data setup complete");
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
        int salesLevel = 0;
        foreach (var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == "sales")
            {
                salesLevel = upgrade.currentLevel;
            }
        }
        for (int i = 0; i < fishTypeCount; i++)
        {
            Debug.Log("on load, fishtypecount is " + fishTypeCount.ToString());
            fishes[i].totalCaught = saveData.totalCaught[i];
            fishes[i].totalSold = saveData.totalSold[i];
            fishes[i].price = Mathf.Round(fishes[i].price * (Mathf.Pow(salesMultiplier, salesLevel -1)));
            Debug.Log("fish " +  fishes[i].index.ToString() + " has data " + fishes[i].totalCaught.ToString() + " and " + fishes[i].totalSold.ToString());

            allFishCaught += fishes[i].totalCaught;
            if (fishes[i].rarity == 3 && fishes[i].totalCaught > 0)
            {
                rareCaught += 1;
            }
        }

        money = saveData.money;

    }
}
