using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

// for animations that take place during dialogues
public class commandExtend_anims : MonoBehaviour
{
    public GameObject overlord;
    public Material none;
    public Sprite overlordDim;
    public Sprite overlordBright;

    public GameObject dialoguePanel;
    // Start is called before the first frame update
    void Start()
    {
        commandManager.instance.commandData.Add("shake", new Action(Shake));

        commandManager.instance.commandData.Add("ungray", new Action(ungray));
        commandManager.instance.commandData.Add("shakeBoss", new Action(shakeBoss));
        commandManager.instance.commandData.Add("shakeBossStrong", new Action(shakeBossStrong));
        commandManager.instance.commandData.Add("moveUp", new Action(moveUp));
        commandManager.instance.commandData.Add("moveDown", new Action(moveDown));
    }

    public void Shake()
    {
        iTween.ShakePosition(dialoguePanel, new Vector3(25, 25, 25), 0.25f);
    }

    public void ungray()
    {
        overlord.GetComponent<Image>().material = none;
    }

    public void shakeBoss()
    {
        iTween.ShakePosition(overlord, new Vector3(25, 25, 25), 0.3f);
    }

    public void shakeBossStrong()
    {
        iTween.ShakePosition(overlord, new Vector3(40, 40, 40), 0.5f);
    }

    public void moveUp()
    {
        iTween.MoveBy(overlord, iTween.Hash("amount", new Vector3(0, 25, 0), "time", 0.2f, "easetype", "easeOutSine"));
        overlord.GetComponent<Image>().sprite = overlordBright;
    }

    public void moveDown()
    {
        iTween.MoveBy(overlord, iTween.Hash("amount", new Vector3(0, -25, 0), "time", 0.2f, "easetype", "easeOutSine"));
        overlord.GetComponent<Image>().sprite = overlordDim;
    }
}
