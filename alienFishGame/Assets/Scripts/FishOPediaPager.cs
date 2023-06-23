using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOPediaPager : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
    public GameObject activePage;

    // Audio
    public FMODUnity.EventReference uiFishopediaPageLeftEvent;
    public FMODUnity.EventReference uiFishopediaPageRightEvent;
    public FMODUnity.EventReference uiFishopediaCloseEvent;

    // Start is called before the first frame update
    void Start()
    {

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
        activePage.SetActive(true);
        LeanTween.scale(activePage, new Vector3(1, 1, 1), 0.15f);
    }

    public void Close()
    {
        LeanTween.scale(activePage, new Vector3(0, 0, 0), 0.15f).setOnComplete(onComplete);
        FMODUnity.RuntimeManager.PlayOneShot(uiFishopediaCloseEvent, transform.position);
    }

    void onComplete()
    {
        activePage.SetActive(false);
    }
}
