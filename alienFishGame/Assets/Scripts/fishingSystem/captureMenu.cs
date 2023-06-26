using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class captureMenu : MonoBehaviour
{
    public GameObject fishIcon;
    public GameObject blankIcon;
    public GameObject scrollPanel;
    public GameObject fishingrods;
    public Transform bgGrid;
    public GameObject iconBG;

    
    public List<int> fishList;

    public void UpdateInfo(List<int> fishCaught)
    {
        // clear out the panel first
        ClearChildren();

        fishList = fishCaught;
        foreach (var index in fishCaught)
        {
            GameObject listItem = Instantiate(fishIcon, new Vector3(0, 0, 0), Quaternion.identity, scrollPanel.transform);

            FishIcon icon = listItem.GetComponent<FishIcon>();
            icon.index = index;
            icon.UpdateFishDisplayed();
            icon.starContainer.gameObject.SetActive(true);

            Instantiate(iconBG, new Vector3(0, 0, 0), Quaternion.identity, bgGrid);
        }

        // hacky fix for the scrollbar 
        // for (int i = 0; i < 9 - counter; i++)
        // {
        //     Instantiate(blankIcon, new Vector3(0, 0, 0), Quaternion.identity, scrollPanel.transform);
        // }
    }

    public void ClearChildren()
    {
        foreach (Transform child in scrollPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in bgGrid)
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
        LeanTween.scale(gameObject, new Vector3(0,0,0), 0.15f).setOnComplete(OnComplete);
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

    void OnComplete()
    {
        gameObject.SetActive(false);
    }

}
