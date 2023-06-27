using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishIcon : MonoBehaviour
{
    public int index;
    public Fish fish;
    public Image image;
    public Sprite sprite;
    public GameObject infoPanel;

    public GameObject star;
    public Transform starContainer;

    public TextMeshProUGUI numberOwned;
    public GameObject stackDisplay;

    public GameObject hoverLabelPrefab;
    public GameObject hoverLabel;

    public Material grayOut;
    public Material none;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());
        image.sprite = sprite;
        fish = FishDataManager.instance.GetFish(index);
    }

    // Update is called once per frame
    // mainly to handle fishopedia shenanigans
    void Update()
    {
        if (fish.totalCaught == 0 && infoPanel != null)
        {
            image.material = grayOut;
        }
        else
        {
            image.sprite = sprite;
            image.material = none;
        }
    }

    public void UpdateFishDisplayed()
    {
        sprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());
        image.sprite = sprite;
        fish = FishDataManager.instance.GetFish(index);

        foreach (Transform child in starContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        for (var i = 0; i < fish.rarity; i++)
        {
            Instantiate(star, new Vector3(0,0,0), Quaternion.identity, starContainer);
        }

        numberOwned.text = (fish.totalCaught - fish.totalSold).ToString();
    }

    public void OnHoverEnter()
    {
        LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);

        if (infoPanel != null && fish.totalCaught == 0)
        {
            return;
        }

        // name displayed when you hover over the icon
        // yeah that's a lot of calcs to figure out the position of the label
        var height = GetComponent<RectTransform>().sizeDelta.y;
        hoverLabel = Instantiate(hoverLabelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        var rect = hoverLabel.GetComponent<RectTransform>();
        var pos = rect.anchoredPosition;
        rect.anchoredPosition = new Vector2(pos.x, pos.y + height/2);

        var text = hoverLabel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = FishDataManager.instance.GetFish(index).name;
    }

    public void OnHoverExit()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
        GameObject.Destroy(hoverLabel);
    }

    public void OnClick()
    {
        if (infoPanel != null && fish.totalCaught > 0)
        {
            infoPanel.SetActive(true);
            infoPanel.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(infoPanel, new Vector3(1, 1, 1), 0.2f);
            infoPanel.GetComponent<infoPanel>().UpdateInfo(index);
        }
    }

    public void OnExit()
    {
        LeanTween.scale(infoPanel, new Vector3(0, 0, 0), 0.2f).setOnComplete(OnTweenComplete);
    }

    void OnTweenComplete()
    {
        infoPanel.SetActive(false);
    }
}

