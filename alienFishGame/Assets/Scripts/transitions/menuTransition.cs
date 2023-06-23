using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuTransition : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        iTween.ValueTo(panel, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        bgmScript.instance.SetParameter(1f);
        iTween.ValueTo(panel, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "loadNewScene", "oncompletetarget", this.gameObject));
    }

    public void LoadGame(int index)
    {
        bgmScript.instance.SetParameter(1f);
        iTween.ValueTo(panel, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "loadSaveFile", "oncompletetarget", this.gameObject, "oncompleteparams", index));
    }

    void loadSaveFile(int index)
    {
        SaveSystem.instance.Load(index);
    }

    public void loadNewScene()
    {
        SceneManager.LoadScene("Scenes/FishingScene");
    }

    void updateColor(float val)
    {
        Image image = panel.GetComponent<Image>();
        image.color = new Color(0, 0, 0, val);
    }
}
