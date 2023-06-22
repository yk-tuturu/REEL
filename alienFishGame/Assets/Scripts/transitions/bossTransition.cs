using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossTransition : MonoBehaviour
{
    public GameObject panel;
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
        iTween.ValueTo(panel, iTween.Hash("from", 1f, "to", 0f, "time", 1.3f, "onupdate", "updateColor", "onupdatetarget", this.gameObject, "oncomplete", "beginDialogue", "oncompletetarget", this.gameObject));
    }

    void updateColor(float val)
    {
        Debug.Log(val.ToString());
        Image image = panel.GetComponent<Image>();
        image.color = new Color(1, 1, 1, val);
    }

    void beginDialogue()
    {
        var trigger = GameObject.Find("DialogueManager").GetComponent<Dialogue_trigger>();
        trigger.TriggerDialogue("overLordDialogue");
        GameObject.Destroy(panel);
    }
}
