using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform gridContainer;
    public GameObject fishIcon;
    public GameObject blankIcon;

    public GameObject mainPanel;

    // Audio
    public FMODUnity.EventReference uiInventoryOpenEvent;
    public FMODUnity.EventReference uiInventoryCloseEvent;

    // Start is called before the first frame update 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
        mainPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(mainPanel, new Vector3(1, 1, 1), 0.2f);

        FMODUnity.RuntimeManager.PlayOneShot(uiInventoryOpenEvent, transform.position);

        foreach(Transform child in gridContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        int counter = 0;
        for (int i = 0 ; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Fish fish = FishDataManager.instance.GetFish(i);

            if (fish.type == "boss")
            {
                continue;
            }

            if (fish.totalCaught - fish.totalSold > 0)
            {
                GameObject temp = Instantiate(fishIcon, new Vector3(0,0,0), Quaternion.identity, gridContainer);
                FishIcon icon = temp.GetComponent<FishIcon>();
                icon.index = i;
                icon.UpdateFishDisplayed();
                icon.starContainer.gameObject.SetActive(true);
                icon.stackDisplay.SetActive(true);
                
                Image bgSlot = icon.bgGrid.GetComponent<Image>();
                bgSlot.color = new Vector4(1, 1, 1, 1);

                counter += 1;
            }
        }

        // a hacky fix for the scrollbar
        for (int j = 0; j < 18 - counter; j++)
        {
            Instantiate(blankIcon, new Vector3(0, 0, 0), Quaternion.identity, gridContainer);
        }

        // fix for scroll positioning
        if (counter > 18)
        {
            var gridRect = gridContainer.GetComponent<RectTransform>();
            var ogPos = gridRect.anchoredPosition;
            gridRect.anchoredPosition = new Vector2(ogPos.x, ogPos.y - 400);
        }
    }

    public void CloseInventory()
    {
        LeanTween.scale(mainPanel, new Vector3(0, 0, 0), 0.2f).setOnComplete(OnComplete);
        FMODUnity.RuntimeManager.PlayOneShot(uiInventoryCloseEvent, transform.position);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
