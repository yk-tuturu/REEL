using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infoPanel : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Image fishImage;

    public Image infoPanelImage;
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
        Fish fish = FishDataManager.instance.GetFish(index);

        Sprite sprite = Resources.Load<Sprite>("fishPanels/fish-" + index.ToString());

        infoPanelImage.sprite = sprite;
        
        description.text = fish.description;
        
        var fishSprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());

        if (fishSprite != null)
        {
            fishImage.sprite = fishSprite;
        }
    }

    public void Close()
    {
        LeanTween.scale(gameObject, new Vector3(0,0,0), 0.15f).setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
