using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class captureMenu : MonoBehaviour
{
    public GameObject icon;
    public GameObject scrollPanel;
    public GameObject fishingrods;

    // may not need this variable anymore, but just in case
    public List<int> fishList;

    public void UpdateInfo(List<int> fishCaught)
    {
        // clear out the panel first
        ClearChildren();

        fishList = fishCaught;
        foreach (var index in fishCaught)
        {
            GameObject listItem = Instantiate(icon, new Vector3(0, 0, 0), Quaternion.identity, scrollPanel.transform);
            listItem.GetComponent<FishIcon>().index = index;
            listItem.GetComponent<FishIcon>().UpdateFishDisplayed();
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
        FishDataManager.instance.ClaimFish(fishList);

        foreach (Transform child in fishingrods.transform)
        {
            FishingPole rod = child.GetComponent<FishingPole>();
            if (rod.menuOpen)
            {
                rod.fishCaught = new List<int>();
                rod.currentCapacity = 0;
            }
        }

        ClearChildren();
    }

}
