using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellConfirmMenu : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI numberText;
    public Fish fish;
    public int numberToSell = 1;
    public FishIcon fishIcon;
    public SellMenu sellMenu;
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
        numberText.text = "1";

        fishIcon.index = index;

        fishIcon.UpdateFishDisplayed();
    }

    public void AddFish()
    {
        numberToSell = Mathf.Min(numberToSell + 1, fish.totalCaught - fish.totalSold);
        numberText.text = numberToSell.ToString();
        priceText.text = (fish.price * numberToSell).ToString();
    }

    public void RemoveFish()
    {
        numberToSell = Mathf.Max(numberToSell - 1, 1);
        numberText.text = numberToSell.ToString();
        priceText.text = (fish.price * numberToSell).ToString();
    }

    public void SellFish()
    {
        FishDataManager.instance.SellFish(fish.index, numberToSell);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);
        sellMenu.UpdateSellInfo();
    }

    public void Cancel()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
