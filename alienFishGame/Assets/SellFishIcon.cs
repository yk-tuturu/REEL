using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellFishIcon : MonoBehaviour
{
    public int index;
    public Fish fish;
    public Image image;
    public Sprite sprite;
    public GameObject sellConfirmMenu;

    public FishDataManager fishData;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        sprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());
        fish = FishDataManager.instance.GetFish(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (fish.totalCaught > 0)
        {
            image.sprite = sprite;
        }
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
