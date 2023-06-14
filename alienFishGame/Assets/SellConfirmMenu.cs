using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellConfirmMenu : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public Fish fish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo(int index)
    {
        fish = FishDataManager.instance.GetFish(index);
        priceText.text = fish.price.ToString();
    }

    public void AddFish()
    {
        
    }

    public void RemoveFish()
    {

    }


}
