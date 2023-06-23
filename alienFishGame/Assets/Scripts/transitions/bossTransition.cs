using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossTransition : MonoBehaviour
{
    public Image panel;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        iTween.ValueTo(panel.gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1.3f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "beginDialogue", "oncompletetarget", this.gameObject));
    }

    public void FadeToBlack()
    {
        panel.color = new Color(0, 0, 0, 0);
        iTween.ValueTo(panel.gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "LoadMenu", "oncompletetarget", this.gameObject));
    }

    void updateColor(float val)
    {
        Debug.Log(val.ToString());
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, val);
    }

    void beginDialogue()
    {
        var trigger = GameObject.Find("DialogueManager").GetComponent<Dialogue_trigger>();
        trigger.TriggerDialogue("overLordDialogue");
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
