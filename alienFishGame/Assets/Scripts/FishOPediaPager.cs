using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOPediaPager : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
    public GameObject activePage;
    public GameObject overlordSprite;

    // Audio
    public FMODUnity.EventReference uiFishopediaPageLeftEvent;
    public FMODUnity.EventReference uiFishopediaPageRightEvent;
    public FMODUnity.EventReference uiFishopediaCloseEvent;

    public FMODUnity.EventReference displaySnapshot;
    FMOD.Studio.EventInstance displayInstance;

    // Start is called before the first frame update
    void Start()
    {
        if (FishDataManager.instance.bossDefeated)
        {
            overlordSprite.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        activePage = page2;
        FMODUnity.RuntimeManager.PlayOneShot(uiFishopediaPageRightEvent, transform.position);
    }

    public void PreviousPage()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        activePage = page1;
        FMODUnity.RuntimeManager.PlayOneShot(uiFishopediaPageLeftEvent, transform.position);
    }

    public void Open()
    {
        displayInstance = FMODUnity.RuntimeManager.CreateInstance(displaySnapshot);
        displayInstance.start();

        activePage.SetActive(true);
        activePage.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(activePage, new Vector3(1, 1, 1), 0.15f);
    }

    public void Close()
    {
        displayInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        displayInstance.release();
        
        LeanTween.scale(activePage, new Vector3(0, 0, 0), 0.15f).setOnComplete(onComplete);
        FMODUnity.RuntimeManager.PlayOneShot(uiFishopediaCloseEvent, transform.position);
    }

    void onComplete()
    {
        activePage.SetActive(false);
    }
}
