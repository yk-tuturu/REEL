using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;  
using System.Text;

public class ShopScreen : MonoBehaviour
{
    public GameObject bgPanel;
    public TextMeshProUGUI dialogueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        LeanTween.scale(bgPanel, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);
    }

    public void OnComplete()
    {
        gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        bgPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(bgPanel, new Vector3(1, 1, 1), 0.15f);

        // reads the text file
        TextAsset file = (TextAsset)Resources.Load("Story/shopkeep");
        List<string> sentenceList = new List<string>();
        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sentenceList.Add(line);
            }
        }

        // picks a random line
        var index = Random.Range(0, sentenceList.Count);
        StartCoroutine(TypeSentence(sentenceList[index]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // play text audio here
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
