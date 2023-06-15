using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class purchaseMenu : MonoBehaviour
{
    public string type;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    public Transform fishingRodParent;
    public Transform upgradeParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePurchaseInfo(string updateType, int price, string description)
    {
        type = updateType;
        descriptionText.text = description;
        priceText.text = "Price: " + price.ToString();
    }

    public void Close()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);
    }

    public void Purchase()
    {
        // I am aware of how yandev-style this is but pls bear with me
        // unfortunately i cant think of any better way to do this
        // maybe will change this to a switch statement later

        if (type == "rod")
        {
            RodStatManager.instance.UpgradeRod();
        }
        else if (type == "bait")
        {
            RodStatManager.instance.UpgradeBait();
            TrapStatManager.instance.UpgradeBait();
        }
        else if (type == "trap")
        {
            TrapStatManager.instance.UpgradeTrap();
        }
        else if (type == "sales")
        {
            // code to upgrade sales
        }

        // closes menu
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);

        // refreshes stats
        foreach(Transform child in fishingRodParent)
        {
            FishingPole rod = child.GetComponent<FishingPole>();
            rod.fetchStats();
        }

        foreach(Transform child in upgradeParent)
        {
            Upgrades upgrade = child.GetComponent<Upgrades>();
            if (type == upgrade.upgradeType)
            {
                upgrade.nextLevel += 1;
                if (upgrade.nextLevel > upgrade.maxLevel)
                {
                    upgrade.enabled = false;
                    child.GetComponent<EventTrigger>().enabled = false;
                }
                // prolly also increase price here
            }
        }
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
