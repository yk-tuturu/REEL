using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellFishIcon : MonoBehaviour
{
    public int index;
    public Fish fish;
    public Image image;
    public Sprite sprite;
    public GameObject sellConfirmMenu;
    public GameObject star;
    public Transform starContainer;
    public TextMeshProUGUI numberOwned;

    public FishDataManager fishData;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        sprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());
        fish = FishDataManager.instance.GetFish(index);
    }

    public void UpdateFishDisplayed()
    {
        image = GetComponent<Image>();
        sprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());
        image.sprite = sprite;
        fish = FishDataManager.instance.GetFish(index);
        numberOwned.text = (fish.totalCaught - fish.totalSold).ToString();

        for (var i = 0; i < fish.rarity; i++)
        {
            Instantiate(star, new Vector3(0,0,0), Quaternion.identity, starContainer);
        }

        // very skull emoji line of code
        sellConfirmMenu = transform.parent.parent.parent.parent.Find("sellConfirmMenu").gameObject;
    }

    public void OnHoverEnter()
    {
        LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
    }

    public void OnHoverExit()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
    }

    public void OnClick()
    {
        sellConfirmMenu.SetActive(true);
        sellConfirmMenu.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(sellConfirmMenu, new Vector3(1, 1, 1), 0.2f);
        sellConfirmMenu.GetComponent<SellConfirmMenu>().UpdateInfo(index);
    }

    public void OnExit()
    {
        LeanTween.scale(sellConfirmMenu, new Vector3(0, 0, 0), 0.2f).setOnComplete(OnTweenComplete);
        
    }

    void OnTweenComplete()
    {
        sellConfirmMenu.SetActive(false);
    }
}
