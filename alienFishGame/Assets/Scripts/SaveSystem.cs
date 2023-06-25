using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;


// have to use this to circumvent the fact that WE CANT FUCKING PUT A DICT IN A JSON FILE
[System.Serializable]
public struct Upgrade{
    public string type;
    public int currentLevel;
}



public class SaveSystem : MonoBehaviour
{
    // [DllImport("__Internal")]
    // private static extern void JS_FileSystem_Sync();

    public string saveFolder;
    public int loadIndex;
    public Upgrades[] upgradeList;
    public FishingPole[] fishingRodList;
    public SaveData currentSaveData;
    public static SaveSystem instance;

    public float autosaveTime = 2f;
    private float timer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }  

        DontDestroyOnLoad(this.gameObject);
        Debug.Log("awake function called");

        saveFolder = "idbfs/alienFishSaves69420727";

        Debug.Log(saveFolder);
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory("idbfs/alienFishSaves69420727");
        }
    }

    void Start()
    {
        Debug.Log("start function called");
        // change this to persistent file path later
        
        // remove this line later!!!!
        // LoadData();
        
    }

    void Update()
    {
        // autosaves
        timer += Time.deltaTime;
        if (timer >= autosaveTime)
        {
            Save(0);
            timer = 0;
        }
    }

    public void Save(int index = 0)
    {
        if (!SceneManager.GetSceneByName("FishingScene").isLoaded)
        {
            return;
        }

        List<int> totalCaught = new List<int>();
        List<int> totalSold = new List<int>();
        for (int i = 0; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            totalCaught.Add(fish.totalCaught);
            totalSold.Add(fish.totalSold);
        }

        upgradeList = FindObjectsByType<Upgrades>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        fishingRodList = FindObjectsByType<FishingPole>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        List<Upgrade> upgradeLevels = new List<Upgrade>();
        foreach (var upgrade in upgradeList)
        {
            Upgrade temp = new Upgrade() {
                type = upgrade.upgradeType,
                currentLevel = upgrade.currentLevel
            };
            upgradeLevels.Add(temp);
        }

        float money = FishDataManager.instance.money;

        List<int> Rod0Capacity = new List<int>();
        List<int> Rod1Capacity = new List<int>();
        List<int> Trap0Capacity = new List<int>();
        List<int> Trap1Capacity = new List<int>();
        foreach (var item in fishingRodList)
        {
            if (item.index == 0)
            {
                if (item.type == "rod")
                {
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
            date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy  HH:mm"),
            saveIndex = index,
            bossDefeated = FishDataManager.instance.bossDefeated
        };

        string jsonString = JsonUtility.ToJson(saveData);
        string savePath = saveFolder + "/save" + index.ToString() + ".json";
        File.WriteAllText(savePath, jsonString);;

        // JS_FileSystem_Sync();
        // Debug.Log("files synced!");
    }

    public void Load(int index = 0)
    {
        string savePath = saveFolder + "/save" + index.ToString() + ".json";
        Debug.Log(savePath);
        if (!File.Exists(savePath))
        {
            Debug.Log("save file could not be found!");
            return;
        }

        loadIndex = index;

        // reloads scene to refresh everything
        SceneManager.LoadScene("Scenes/FishingScene");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadData();
    }

    public void LoadData()
    {
        string savePath = saveFolder + "/save" + loadIndex.ToString() + ".json";
        Debug.Log(savePath);
        if (!File.Exists(savePath))
        {
            Debug.Log("save file could not be found upon reload");
            return;
        }

        upgradeList = FindObjectsByType<Upgrades>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        fishingRodList = FindObjectsByType<FishingPole>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        string jsonString = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);
        Debug.Log(jsonString);
        Debug.Log(saveData);

        currentSaveData = saveData;

        FishDataManager.instance.LoadData(currentSaveData);
        RodStatManager.instance.LoadData(currentSaveData);
        TrapStatManager.instance.LoadData(currentSaveData);

        foreach (var upgrade in upgradeList)
        {
            upgrade.LoadData(saveData);
        }

        bool extraRod = false;
        bool extraTrap = false;
        bool skipToBoss = false;
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
            if (upgrade.type == "bait" && upgrade.currentLevel == 4 && !saveData.bossDefeated)
            {
                skipToBoss = true;
            }
        }

        foreach (var rod in fishingRodList)
        {
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

        Debug.Log("load completed!");

        if (skipToBoss)
        {
            var transition = GameObject.Find("Transitions");
            transition.GetComponent<transitions>().transitionToBossFight();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
