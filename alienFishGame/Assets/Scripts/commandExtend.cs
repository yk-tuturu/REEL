using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class commandExtend : MonoBehaviour
{
    public GameObject attackPanel;
    public GameObject dialoguePanel;
    public GameObject choicePanel;
    // Start is called before the first frame update
    void Start()
    {
        commandManager.instance.commandData.Add("debug", new Action(DebugTest));
        commandManager.instance.commandData.Add("shake", new Action(Shake));
        commandManager.instance.commandData.Add("onIntroComplete", new Action(onIntroComplete));
        commandManager.instance.commandData.Add("onAttackLanded", new Action(onAttackLanded));
        commandManager.instance.commandData.Add("onAttackFinished", new Action(onAttackFinished));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DebugTest()
    {
        Debug.Log("testing if delegate works");
    }

    public void Shake()
    {
        iTween.ShakePosition(dialoguePanel, new Vector3(15, 15, 15), 0.25f);
    }

    public void onIntroComplete()
    {
        Debug.Log("introCompleteCalled");
        attackPanel.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    public void onAttackLanded()
    {
        attackPanel.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    public void onAttackFinished()
    {
        choicePanel.SetActive(true);
    }
}
