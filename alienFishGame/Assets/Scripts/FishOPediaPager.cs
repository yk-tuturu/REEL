using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOPediaPager : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
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
    }

    public void PreviousPage()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
