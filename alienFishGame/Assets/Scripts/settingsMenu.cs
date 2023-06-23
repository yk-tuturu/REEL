using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class settingsMenu : MonoBehaviour
{
    public float autosaveTime = 2f;
    private float timer;

    public List<TextMeshProUGUI> saveDates = new List<TextMeshProUGUI>();
    public List<Image> saveImages = new List<Image>();

    public Sprite bgImage;

    public GameObject mainPanel;

    // Audio
    public FMODUnity.EventReference uiSettingsCloseEvent;

    // Start is called before the first frame update
    void Start()
    {
        // fetch previous saves
        FetchSaves();

    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        LeanTween.scale(mainPanel, new Vector3(1, 1, 1), 0.15f);
        FetchSaves();
    }

    public void CloseMenu()
    {
        LeanTween.scale(mainPanel, new Vector3(0, 0, 0), 0.15f).setOnComplete(onComplete);
        FMODUnity.RuntimeManager.PlayOneShot(uiSettingsCloseEvent, transform.position);
    }

    void onComplete()
    {
        gameObject.SetActive(false); 
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void Save(int index)
    {
        SaveSystem.instance.Save(index);
        saveDates[index].text = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy  HH:mm");
        saveImages[index].sprite = bgImage;
    }

    public void Load(int index)
    {
        Debug.Log("loading file at index " + index.ToString());
        SaveSystem.instance.Load(index);
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
}
