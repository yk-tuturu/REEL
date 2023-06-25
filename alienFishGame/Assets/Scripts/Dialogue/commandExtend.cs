using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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
        commandManager.instance.commandData.Add("backToMenu", new Action(backToMenu));

        commandManager.instance.commandData.Add("set10", new Action(set10));
        commandManager.instance.commandData.Add("set11", new Action(set11));
        commandManager.instance.commandData.Add("set12", new Action(set12));
        commandManager.instance.commandData.Add("set13", new Action(set13));
        commandManager.instance.commandData.Add("set13.5", new Action(set13half));
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

    public void backToMenu()
    {
        // gets autosave file and sets boss cleared to true
        string saveFolder = SaveSystem.instance.saveFolder;
        if (Directory.Exists(saveFolder))
        {
            string[] files = Directory.GetFiles(saveFolder);
            foreach (var file in files)
            {
                string jsonString = File.ReadAllText(file);
                SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);

                if (saveData.saveIndex == 0)
                {
                    Debug.Log("found autosave file!");
                    saveData.bossDefeated = true;

                    if (bgmScript.instance.GetParameter() == 13f)
                    {
                        saveData.money += 5000;
                    }

                    Debug.Log(jsonString);
                    jsonString = JsonUtility.ToJson(saveData);
                    File.WriteAllText(file, jsonString);
                }
            }
        }

        var transition = GameObject.Find("transitions");
        if (transition != null)
        {
            transition.GetComponent<bossTransition>().FadeToBlack();
        }
    }

    // this aint the most elegant of code, but im too lazy to reconfigure this to take in an argument
    public void set10()
    {
        bgmScript.instance.SetParameter(10);
    }

    public void set11()
    {
        bgmScript.instance.SetParameter(11);
    }

    public void set12()
    {
        bgmScript.instance.SetParameter(12);
    }

    public void set13()
    {
        bgmScript.instance.SetParameter(13);
    }

    public void set13half()
    {
        bgmScript.instance.SetParameter(13.5f);
    }
}
