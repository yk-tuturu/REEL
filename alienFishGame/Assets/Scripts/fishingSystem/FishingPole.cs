using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

// I dubbed this the fishing pole script but it functions for both rods and traps
public class FishingPole : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    public float timeToNextFish;
    public int currentCapacity;
    public int maxCapacity;
    public string type;
    public int index;

    public int rodLevel;
    public int trapLevel;
    public int baitLevel;

    // fish rarity rng
    public float uncommonProb;
    public float rareProb;

    public GameObject panel;
    public TextMeshProUGUI panel_text;
    
    public GameObject capturesMenu;
    public bool menuOpen;

    public GameObject plusOneLabel;
    public Transform labelSpawner;

    private float timer;

    private Vector3 ogScale;
    
    // dictionary, key is the fishIndex and value is how many caught
    public List<int> fishCaught = new List<int>();

    // Audio
    public FMODUnity.EventReference fishPoleClickEvent;
    public FMODUnity.EventReference fishTrapClickEvent;
    public FMODUnity.EventReference fishCatchSmallCommonEvent;
    public FMODUnity.EventReference fishCatchSmallUncommonEvent;
    public FMODUnity.EventReference fishCatchSmallRareEvent;
    public FMODUnity.EventReference fishCatchLargeCommonEvent;
    public FMODUnity.EventReference fishCatchLargeUncommonEvent;
    public FMODUnity.EventReference fishCatchLargeRareEvent;

    // Start is called before the first frame update
    void Start()
    {
        fetchStats();

        ogScale = transform.localScale;
        timeToNextFish = Random.Range(minTime, maxTime);
        panel_text = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCapacity < maxCapacity)
        {
            timer += Time.deltaTime;
        }
        
        if (timer >= timeToNextFish)
        {
            timer = 0;
            timeToNextFish = Random.Range(minTime, maxTime);

            float rng = Random.value;
            if (rng < rareProb)
            {
                AddFish(4, 4);
            }
            else if (rng < uncommonProb)
            {
                AddFish(3, 3);
            }
            else 
            {
                AddFish(0, 3);
            }
        }
        panel_text.text = currentCapacity.ToString() + "/" + maxCapacity.ToString();
    }

    // might need to change this to take in a list of fish afterwards
    void AddFish(int minIndex, int maxIndex)
    {
        int fishIndex = Random.Range(minIndex, maxIndex);

        fishCaught.Add(fishIndex);

        currentCapacity += 1;

        // Possible code to differentiate large & small fish, rare fish
        FMODUnity.RuntimeManager.PlayOneShot(fishCatchSmallCommonEvent, transform.position);

        // If the menu was open while a fish is caught
        if (menuOpen)
        {
            capturesMenu.GetComponent<captureMenu>().UpdateInfo(fishCaught);
        }

        // Summons the +1
        GameObject temp = Instantiate(plusOneLabel, labelSpawner.position, Quaternion.identity, panel.transform.parent);
        temp.transform.localPosition = labelSpawner.localPosition;
        temp.GetComponent<tween>().tweenIn();
    }

    void OnMouseEnter()
    {
        LeanTween.scale(panel, new Vector3(1.2f, 1.2f, 1.2f), 0.1f);

        
        var endScale = ogScale * 1.1f; 
        LeanTween.scale(gameObject, endScale, 0.1f);
    }

    void OnMouseExit()
    {
        LeanTween.scale(panel, new Vector3(1f, 1f, 1f), 0.1f);
        LeanTween.scale(gameObject, ogScale, 0.1f);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // make sure you dont accidentally click on the fishing rod while on a UI element
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                return;
            }
            capturesMenu.SetActive(true);
            capturesMenu.transform.localScale = new Vector3 (0, 0, 0);

            LeanTween.scale(capturesMenu, new Vector3(1, 1 ,1), 0.2f);

            // some code to update the menu info
            capturesMenu.GetComponent<captureMenu>().UpdateInfo(fishCaught);
            menuOpen = true;

            if (type == "rod")
            {
                FMODUnity.RuntimeManager.PlayOneShot(fishPoleClickEvent, transform.position);
            }
            else if (type == "trap")
            {
                FMODUnity.RuntimeManager.PlayOneShot(fishTrapClickEvent, transform.position);
            }
        }
    } 

    public void fetchStats()
    {
        if (type == "rod")
        {
            minTime = RodStatManager.instance.minTime;
            maxTime = RodStatManager.instance.maxTime;
            maxCapacity = RodStatManager.instance.maxCapacity;
            rodLevel = RodStatManager.instance.rodLevel;
            baitLevel = RodStatManager.instance.baitLevel;
            uncommonProb = RodStatManager.instance.uncommonProb;
            rareProb = RodStatManager.instance.rareProb;
        }
        else if (type == "trap")
        {
            minTime = TrapStatManager.instance.minTime;
            maxTime = TrapStatManager.instance.maxTime;
            maxCapacity = TrapStatManager.instance.maxCapacity;
            trapLevel = TrapStatManager.instance.trapLevel;
            baitLevel = TrapStatManager.instance.baitLevel;
            uncommonProb = TrapStatManager.instance.uncommonProb;
            rareProb = TrapStatManager.instance.rareProb;
        }
    }  
}
