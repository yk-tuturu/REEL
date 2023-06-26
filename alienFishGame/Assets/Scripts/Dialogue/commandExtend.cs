using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class commandExtend : MonoBehaviour
{
    public GameObject attackPanel;
    public GameObject dialoguePanel;
    public GameObject choicePanel;

    public GameObject overlord;
    public Material none;
    public Sprite overlordDim;
    public Sprite overlordBright;

    public TextMeshProUGUI endingText;
    public DialogueManager dialogueManager;
    public Image blackPanel;

    public GameObject attackCG;
    public GameObject releaseCG;
    public TextMeshProUGUI thanksText;

    
    // Start is called before the first frame update
    void Start()
    {
        commandManager.instance.commandData.Add("onIntroComplete", new Action(onIntroComplete));
        commandManager.instance.commandData.Add("onAttackLanded", new Action(onAttackLanded));
        commandManager.instance.commandData.Add("onAttackFinished", new Action(onAttackFinished));
        commandManager.instance.commandData.Add("backToMenu", new Action(backToMenu));

        commandManager.instance.commandData.Add("transitionToAttackCG", new Action(transitionToAttackCG));
        commandManager.instance.commandData.Add("transitionToReleaseCG", new Action(transitionToReleaseCG));
        commandManager.instance.commandData.Add("waitToEnd", new Action(waitToEnd));
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
                    saveData.totalCaught[21] = 1;

                    if (bgmScript.instance.GetParameter() == 13f)
                    {
                        saveData.money += 5000;
                    }

                    Debug.Log(jsonString);
                    jsonString = JsonUtility.ToJson(saveData);
                    File.WriteAllText(file, jsonString);
                }
            }
            bgmScript.instance.Reset();
        }

        var transition = GameObject.Find("transitions");
        if (transition != null && bgmScript.instance.GetParameter() == 13f)
        {
            transition.GetComponent<bossTransition>().FadeToBlack();
        }
        else
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    public void transitionToAttackCG()
    {
        StartCoroutine(fadeToCG("attack"));
    }

    public void transitionToReleaseCG()
    {
        StartCoroutine(fadeToCG("release"));
    }

    IEnumerator fadeToCG(string endingType)
    {
        dialogueManager.speechText = endingText;
        
        
        iTween.ValueTo(blackPanel.gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));

        yield return new WaitForSeconds(1.05f);

        overlord.SetActive(false);
        dialoguePanel.SetActive(false);

        if (endingType == "attack")
        {
            attackCG.SetActive(true);
        }
        else if (endingType == "release")
        {
            releaseCG.SetActive(true);
        }

        iTween.ValueTo(blackPanel.gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));
    }

    // on update functions for the tweens, just ignore them
    void updateColor(float val)
    {
        blackPanel.color = new Color(blackPanel.color.r, blackPanel.color.g, blackPanel.color.b, val);
    }

    void updateColorText(float val)
    {
        thanksText.color = new Color(1, 1, 1, val);
    }

    public void waitToEnd()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        blackPanel.color = new Vector4(0, 0, 0, 0);
        thanksText.gameObject.SetActive(true);
        iTween.ValueTo(blackPanel.gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 3f, "onupdate", "updateColor", "onupdatetarget", this.gameObject));
        iTween.ValueTo(thanksText.gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 3f, "delay", 2f, "onupdate", "updateColorText", "onupdatetarget", this.gameObject));
        yield return new WaitForSeconds(12f);
        iTween.ValueTo(thanksText.gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 3f, "delay", 2f, "onupdate", "updateColorText", "onupdatetarget", this.gameObject));
        yield return new WaitForSeconds(3f);

        backToMenu();
    }
}
