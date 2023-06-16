using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// have to use this to circumvent the fact that WE CANT FUCKING PUT A DICT IN A JSON FILE
[System.Serializable]
public struct Upgrade{
    public string type;
    public int currentLevel;
}

public class SaveSystem : MonoBehaviour
{
    public Transform fishingRodParent;
    public Transform upgradeParent;
    public string saveFolder;
    // Start is called before the first frame update
    void Start()
    {
        // change this to persistent file path later
        saveFolder = Application.dataPath + "/Saves";
        
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        List<int> totalCaught = new List<int>();
        List<int> totalSold = new List<int>();
        for (int i = 0; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            totalCaught.Add(fish.totalCaught);
            totalSold.Add(fish.totalSold);
        }

        List<Upgrade> upgradeLevels = new List<Upgrade>();
        foreach (Transform child in upgradeParent)
        {
            Upgrades upgrade = child.GetComponent<Upgrades>();
            Upgrade temp = new Upgrade() {
                type = upgrade.upgradeType,
                currentLevel = upgrade.nextLevel - 1,
            };
            upgradeLevels.Add(temp);
        }

        float money = FishDataManager.instance.money;

        List<int> Rod0Capacity = new List<int>();
        List<int> Rod1Capacity = new List<int>();
        List<int> Trap0Capacity = new List<int>();
        List<int> Trap1Capacity = new List<int>();
        foreach (Transform child in fishingRodParent)
        {
            FishingPole item = child.GetComponent<FishingPole>();
            if (item.index == 0)
            {
                if (item.type == "rod")
                {
                    Debug.Log("rod saved");
                    Rod0Capacity = item.fishCaught;
                }
                else if (item.type == "trap")
                {
                    Trap0Capacity = item.fishCaught;
                }
            }

            else if (item.index == 1)
            {
                if (item.type == "rod")
                {
                    Rod1Capacity = item.fishCaught;
                    
                }
                else if (item.type == "trap")
                {
                    Trap1Capacity = item.fishCaught;
                }
            }
        }

        SaveData saveData = new SaveData {
            totalCaught = totalCaught,
            totalSold = totalSold,
            upgradeLevels = upgradeLevels,
            money = money,
            Rod0Capacity = Rod0Capacity,
            Rod1Capacity = Rod1Capacity,
            Trap0Capacity = Trap0Capacity,
            Trap1Capacity = Trap1Capacity,
        };

        string jsonString = JsonUtility.ToJson(saveData);
        string savePath = saveFolder + "/save0.json";
        File.WriteAllText(savePath, jsonString);
        Debug.Log("saved!");
    }

    public void Load(int index = 0)
    {
        string savePath = saveFolder + "/save" + index.ToString() + ".json";
        if (!File.Exists(savePath))
        {
            Debug.Log("save file could not be found!");
            return;
        }

        string jsonString = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);
        Debug.Log(jsonString);
        Debug.Log(saveData);

        // implement actually entering the data here
        FishDataManager.instance.LoadData(saveData);
        RodStatManager.instance.LoadData(saveData);
        TrapStatManager.instance.LoadData(saveData);

        foreach (Transform child in upgradeParent)
        {
            child.GetComponent<Upgrades>().LoadData(saveData);
        }

        bool extraRod = false;
        bool extraTrap = false;
        foreach (var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == "extra rod" && upgrade.currentLevel == 2)
            {
                extraRod = true;
            }
            if (upgrade.type == "extra trap" && upgrade.currentLevel == 2)
            {
                extraTrap = true;
            }
        }

        foreach (Transform child in fishingRodParent)
        {
            FishingPole rod = child.GetComponent<FishingPole>();
            rod.fetchStats();
            if (rod.type == "rod")
            {
                if (extraRod)
                {
                    rod.gameObject.SetActive(true);
                    rod.panel.SetActive(true);
                }
                
                if (rod.index == 0)
                {
                    rod.fishCaught = saveData.Rod0Capacity;
                    rod.currentCapacity = rod.fishCaught.Count;
                }

                if (rod.index == 1)
                {
                    rod.fishCaught = saveData.Rod1Capacity;
                    rod.currentCapacity = rod.fishCaught.Count;
                }
            }

            else if (rod.type == "trap")
            {
                if (extraTrap)
                {
                    rod.gameObject.SetActive(true);
                    rod.panel.SetActive(true);
                }
                
                if (rod.index == 0)
                {
                    rod.fishCaught = saveData.Trap0Capacity;
                    rod.currentCapacity = rod.fishCaught.Count;
                }

                if (rod.index == 1)
                {
                    rod.fishCaught = saveData.Trap1Capacity;
                    rod.currentCapacity = rod.fishCaught.Count;
                }
            }
        }

    }
}
