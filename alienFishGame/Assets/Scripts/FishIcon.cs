using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIcon : MonoBehaviour
{
    public Fish fish;
    public Sprite sprite;
    public GameObject infoPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
    }

    public void OnHoverExit()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
    }

    public void OnClick()
    {
        infoPanel.SetActive(true);
        LeanTween.scale(infoPanel, new Vector3(1, 1, 1), 0.2f);
    }

    public void OnExit()
    {
        LeanTween.scale(infoPanel, new Vector3(0, 0, 0), 0.2f).setOnComplete(OnTweenComplete);
        
    }

    void OnTweenComplete()
    {
        infoPanel.SetActive(false);
    }
}

