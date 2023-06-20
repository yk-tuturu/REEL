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

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speechText;
    public GameObject canvas;
    public UnityEvent onDialogueComplete;

    void Awake()
    {
        sentences = new Queue<Dialogue>();
    }

    void Update()
    {
        if (currentlyInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(List<Dialogue> story, UnityEvent onComplete)
    {
        sentences.Clear();
        canvas.SetActive(true);
        currentlyInDialogue = true;
        onDialogueComplete = onComplete;

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
        Dialogue currentLine = sentences.Dequeue();
        speakerText.text = currentLine.speaker;

        currentlyInDialogue = true;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // play text audio here
        speechText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return null;
        }

    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        currentlyInDialogue = false;

        speakerText.text = "";
        speechText.text = "";
        
        if (onDialogueComplete != null)
        {
            onDialogueComplete.Invoke();
        }
    }
}
