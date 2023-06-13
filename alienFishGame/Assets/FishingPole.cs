using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingPole : MonoBehaviour
{
    public float minTime = 2.5f;
    public float maxTime = 4.5f;
    public float timeToNextFish;
    public int currentCapacity;
    public int maxCapacity = 5;

    // fish rarity rng
    public float commonProb = 0.95f;
    public float uncommonProb = 0.045f;
    public float rareProb = 0.005f;

    public GameObject panel;
    public TextMeshProUGUI panel_text;
    public GameObject capturesMenu;
    public FishDataManager fishData;

    private float timer;
    public bool menuOpen;

    // dictionary, key is the fishIndex and value is how many caught
    public Dictionary<int, int> fishCaught = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
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
            if (rng < commonProb)
            {
                AddFish(0, 2);
            }
            else if (rng < uncommonProb + commonProb)
            {
                AddFish(3, 3);
            }
            else 
            {
                AddFish(4, 4);
            }
        }
    }

    void AddFish(int minIndex, int maxIndex)
    {
        int fishIndex = Random.Range(minIndex, maxIndex);

        if (fishCaught.ContainsKey(fishIndex))
        {
            fishCaught[fishIndex] += 1;
        }
        else
        {
            fishCaught.Add(fishIndex, 1);
        }

        currentCapacity += 1;

        // If the menu was open while a fish is caught
        if (menuOpen)
        {
            capturesMenu.GetComponent<captureMenu>().UpdateInfo(fishCaught);
        }
    }


    void OnMouseEnter()
    {
        // come back and implement alpha tween if you have time
        panel.SetActive(true);
    }

    void OnMouseExit()
    {
        panel.SetActive(false);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            capturesMenu.SetActive(true);
            capturesMenu.transform.localScale = new Vector3 (0, 0, 0);

            LeanTween.scale(capturesMenu, new Vector3(1, 1 ,1), 0.2f);

            // some code to update the menu info
            capturesMenu.GetComponent<captureMenu>().UpdateInfo(fishCaught);
            menuOpen = true;
        }

        panel_text.text = currentCapacity.ToString() + " / " + maxCapacity.ToString();
    }
}
