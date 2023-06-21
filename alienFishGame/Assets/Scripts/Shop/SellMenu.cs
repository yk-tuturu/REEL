using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMenu : MonoBehaviour
{
    public GameObject sellFishIcon;
    public Transform sellPanel;
    public GameObject blankIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSellInfo()
    {
        // clear out icons from previous run
        foreach (Transform child in sellPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        var iconCounter = 0;
        for (var i = 0; i < 5; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            if (fish.totalCaught - fish.totalSold > 0)
            {
                GameObject icon = Instantiate(sellFishIcon, new Vector3(0, 0, 0), Quaternion.identity, sellPanel);
                icon.GetComponent<SellFishIcon>().index = i;
                icon.GetComponent<SellFishIcon>().UpdateFishDisplayed();
                iconCounter += 1;
            }
        }

        for (var i = 0; i < 12 - iconCounter; i++)
        {
            GameObject icon = Instantiate(blankIcon, new Vector3(0, 0, 0), Quaternion.identity, sellPanel);
        }
    }

    public void SellAll()
    {
        Debug.Log("selling fish");
        for (var i = 0; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            FishDataManager.instance.SellFish(i, fish.totalCaught - fish.totalSold);
            UpdateSellInfo();
        }
    }
}
