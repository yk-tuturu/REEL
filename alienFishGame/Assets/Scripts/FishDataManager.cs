using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataManager : MonoBehaviour
{
    public static FishDataManager instance;

    public TextAsset jsonFile;
    public Fishes fishList;
    public Fish[] fishes;
    public int fishTypeCount = 5;
    public float money;

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
 
    void Start()
    {
        // reads fish data from json file
        Fishes fishList = JsonUtility.FromJson<Fishes>(jsonFile.text);
 
        foreach (Fish fish in fishList.fishes)
        {
            Debug.Log(fish.name);
            Debug.Log(fish.index);
        }

        fishes = fishList.fishes;
    }

    void Update()
    {
    
    }

    // utility function, probably
    public Fish GetFish(int index)
    {
        Debug.Log(index);
        return (fishes[index]);
    }

    public void ClaimFish(Dictionary<int, int> fishCaught)
    {
        foreach(var fish in fishCaught)
        {
            fishes[fish.Key].totalCaught += fish.Value;
            Debug.Log("You have caught " + fish.Value.ToString() + " " + fishes[fish.Key].name);
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
            fish.price = Mathf.Round(fish.price * 1.7f);
        }
    }
}
