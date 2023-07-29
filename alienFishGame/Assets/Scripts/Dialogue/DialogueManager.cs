using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public Queue<Dialogue> sentences = new Queue<Dialogue>();
    public bool currentlyInDialogue = false;
    public bool currentlyTyping = false;

    public Dialogue currentLine;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speechText;
    public GameObject dialoguePanel;

    void Awake()
    {
        sentences = new Queue<Dialogue>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentlyInDialogue)
        {
            if (currentlyTyping)
            {
                StopAllCoroutines();
                speechText.text = currentLine.sentence;
                currentlyTyping = false;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(List<Dialogue> story)
    {
        sentences.Clear();
        dialoguePanel.SetActive(true);
        currentlyInDialogue = true;

        foreach (Dialogue dialogue in story) {
            sentences.Enqueue(dialogue);    
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Debug.Log("displaying next sentence");
        Debug.Log(sentences.Count.ToString());
        currentLine = sentences.Dequeue();
        speakerText.text = currentLine.speaker;

        foreach (string command in currentLine.commands)
        {
            commandManager.instance.Execute(command);
        }

        currentlyInDialogue = true;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // play text audio here
        speechText.text = "";
        currentlyTyping = true;
        foreach(char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return null;
        }
        currentlyTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        currentlyInDialogue = false;

        speakerText.text = "";
        speechText.text = "";
    }
}
