using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMenu : MonoBehaviour
{
    public GameObject sellFishIcon;
    public Transform sellPanel;
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
        for (var i = 0; i < 5; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);
            if (fish.totalCaught - fish.totalSold > 0)
            {
                GameObject icon = Instantiate(sellFishIcon, new Vector3(0, 0, 0), Quaternion.identity, sellPanel);
                icon.GetComponent<SellFishIcon>().index = i;
            }
        }
    }
}
