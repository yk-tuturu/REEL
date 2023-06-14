using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
    public GameObject plusOneLabel;
    public Vector3 labelPosition;
    public GameObject spawner;

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

        panel_text.text = currentCapacity.ToString() + " / " + maxCapacity.ToString();
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
        panel_text.text = currentCapacity.ToString() + " / " + maxCapacity.ToString();

        // If the menu was open while a fish is caught
        if (menuOpen)
        {
            capturesMenu.GetComponent<captureMenu>().UpdateInfo(fishCaught);
        }

        // Summons the +1
        GameObject temp = Instantiate(plusOneLabel, spawner.transform.position, Quaternion.identity, panel.transform.parent);
        temp.transform.localPosition = spawner.transform.localPosition;
        temp.GetComponent<tween>().tweenIn();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        }

    }   
}
