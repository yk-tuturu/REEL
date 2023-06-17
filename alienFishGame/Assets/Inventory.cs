using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public Transform gridContainer;
    public GameObject fishIcon;
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
        LeanTween.scale(transform.GetChild(0).gameObject, new Vector3(1, 1, 1), 0.2f);
        
        // Destroy stuff from previous iterations
        foreach(Transform child in gridContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0 ; i < FishDataManager.instance.fishTypeCount; i++)
        {
            Debug.Log("instantializing fish");
            Fish fish = FishDataManager.instance.GetFish(i);
            if (fish.totalCaught - fish.totalSold > 0)
            {
                GameObject temp = Instantiate(fishIcon, new Vector3(0,0,0), Quaternion.identity, gridContainer);
                FishIcon icon = temp.GetComponent<FishIcon>();
                icon.index = i;
                icon.UpdateFishDisplayed();
                icon.starContainer.gameObject.SetActive(true);
                icon.stackDisplay.SetActive(true);
            }
        }
    }

    public void CloseInventory()
    {
        LeanTween.scale(transform.GetChild(0).gameObject, new Vector3(0, 0, 0), 0.2f).setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
