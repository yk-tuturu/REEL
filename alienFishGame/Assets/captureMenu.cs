using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class captureMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject scrollPanel;
    public FishDataManager fishData;
    public GameObject fishingrods;

    // may not need this variable anymore, but just in case
    public Dictionary<int, int> fishDict;

    public void UpdateInfo(Dictionary<int, int> fishCaught)
    {
        // clear out the panel first
        ClearChildren();

        fishDict = fishCaught;
        foreach (var fishType in fishCaught)
        {
            GameObject listItem = Instantiate(panel, new Vector3(0, 0, 0), Quaternion.identity, scrollPanel.transform);
            TextMeshProUGUI text = listItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Fish fish = fishData.GetFish(fishType.Key);
            text.text = fish.name + " X " + fishType.Value.ToString();
        }

        // this is just some dumb way to circumvent a certain bug in the scroll bar view
        if (fishCaught.Count < 4)
        {
            for (var i = 0; i < 4 - fishCaught.Count; i++ )
            {
                GameObject temp = Instantiate(panel, new Vector3(0, 0, 0), Quaternion.identity, scrollPanel.transform);
                temp.GetComponent<Image>().sprite = null;
                temp.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
            }
        }
    }

    public void ClearChildren()
    {
        foreach (Transform child in scrollPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Close()
    {
        foreach (Transform child in fishingrods.transform)
        {
            FishingPole rod = child.GetComponent<FishingPole>();
            rod.menuOpen = false;
        }
        gameObject.SetActive(false);
    }

    public void ClaimFish()
    {
        fishData.ClaimFish(fishDict);

        foreach (Transform child in fishingrods.transform)
        {
            FishingPole rod = child.GetComponent<FishingPole>();
            if (rod.menuOpen)
            {
                rod.fishCaught = new Dictionary<int, int>() {};
                rod.currentCapacity = 0;
            }
        }

        ClearChildren();
    }

}
