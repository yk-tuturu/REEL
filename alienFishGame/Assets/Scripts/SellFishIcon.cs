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

    public GameObject bgGrid;

    public FishDataManager fishData;

    public FMODUnity.EventReference clickEvent;

    public GameObject hoverLabelPrefab;
    public GameObject hoverLabel;

    
    // Start is called before the first frame update
    void Start()
    {
        sprite = FishDataManager.instance.fishSpriteDict["fish" + index.ToString()];
        fish = FishDataManager.instance.GetFish(index);
    }

    public void UpdateFishDisplayed()
    {
        sprite = FishDataManager.instance.fishSpriteDict["fish" + index.ToString()];
        
        image.sprite = sprite;
        fish = FishDataManager.instance.GetFish(index);
        numberOwned.text = (fish.totalCaught - fish.totalSold).ToString();

        // clears out any leftover stars
        foreach (Transform child in starContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (var i = 0; i < fish.rarity; i++)
        {
            Instantiate(star, new Vector3(0,0,0), Quaternion.identity, starContainer);
        }

        // very skull emoji line of code
        sellConfirmMenu = transform.parent.parent.parent.parent.Find("sellConfirmMenu").gameObject;
    }

    public void OnHoverEnter()
    {
        LeanTween.scale(bgGrid, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
    }

    public void OnHoverExit()
    {
        LeanTween.scale(bgGrid, new Vector3(1f, 1f, 1f), 0.1f);
    }

    public void OnClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(clickEvent);
        sellConfirmMenu.SetActive(true);
        sellConfirmMenu.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(sellConfirmMenu, new Vector3(1, 1, 1), 0.2f);
        sellConfirmMenu.GetComponent<SellConfirmMenu>().UpdateInfo(index);
    }

    public void OnExit()
    {
        LeanTween.scale(sellConfirmMenu, new Vector3(0, 0, 0), 0.2f).setOnComplete(OnTweenComplete);
        GameObject.Destroy(hoverLabel);
    }

    void OnTweenComplete()
    {
        sellConfirmMenu.SetActive(false);
    }
}
