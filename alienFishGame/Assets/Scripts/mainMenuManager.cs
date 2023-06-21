using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class mainMenuManager : MonoBehaviour
{
    // if there are no save files, set this bool to true
    public bool noSave = false;

    public GameObject confirmPanel;
    public GameObject loadScreen;

    public List<TextMeshProUGUI> saveDates = new List<TextMeshProUGUI>();
    public List<Image> saveImages = new List<Image>();
    public Sprite bgImage;

    // why
    public GameObject panelToBeShrunk;
    // Start is called before the first frame update
    void Start()
    {
        var counter = 0;
        string savePath = SaveSystem.instance.saveFolder;
        if (Directory.Exists(savePath))
        {
            string[] files = Directory.GetFiles(savePath);
            foreach (var file in files)
            {
                if (file.Contains(".meta"))
                {
                    continue;
                }
                counter += 1;
                Debug.Log("reading save file at " + file);
            }
        }

        if (counter <= 0)
        {
            noSave = true;
        }

        bgImage = Resources.Load<Sprite>("placeholderSprites/background");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (noSave == false)
        {
            // asks player to overwrite save files
            confirmPanel.SetActive(true);
            LeanTween.scale(confirmPanel, new Vector3(1,1,1), 0.15f);
        }
        else
        {
            SceneManager.LoadScene("Scenes/FishingScene");
        }
    }

    public void LoadButton()
    {
        FetchSaves();
        loadScreen.SetActive(true);
        LeanTween.scale(loadScreen, new Vector3(1,1,1), 0.15f);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OverwriteSaves()
    {
        // string path = SaveSystem.instance.saveFolder;
 
        // DirectoryInfo directory = new DirectoryInfo(path);
 
        // foreach (FileInfo file in directory.GetFiles()) {
        //     file.Delete();
        // }

        SceneManager.LoadScene("Scenes/FishingScene");
    }

    public void CloseConfirmMenu()
    {
        Shrink(confirmPanel);
    }

    public void FetchSaves()
    {
        string savePath = SaveSystem.instance.saveFolder;
        if (Directory.Exists(savePath))
        {
            string[] files = Directory.GetFiles(savePath);
            foreach (var file in files)
            {
                if (file.Contains(".meta"))
                {
                    continue;
                }
                Debug.Log("reading save file at " + file);
                string jsonString = File.ReadAllText(file);
                SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);
                Debug.Log(jsonString);
                saveDates[saveData.saveIndex].text = saveData.date;
                saveImages[saveData.saveIndex].sprite = bgImage;
            }
        }
    }

    public void Load(int index)
    {
        Debug.Log("loading file at index " + index.ToString());
        SaveSystem.instance.Load(index);
    }

    public void CloseLoadMenu()
    {
        Shrink(loadScreen);
    }

    public void OnHoverEnter(GameObject item)
    {
        LeanTween.scale(item, new Vector3(0.9f, 0.9f, 0.9f), 0.15f);
    }

    public void OnHoverExit(GameObject item)
    {
        LeanTween.scale(item, new Vector3(1,1,1), 0.15f);
    }

    void Shrink(GameObject panel)
    {
        panelToBeShrunk = panel;
        LeanTween.scale(panel, new Vector3(0,0,0), 0.15f).setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        panelToBeShrunk.SetActive(false);
    }
}
