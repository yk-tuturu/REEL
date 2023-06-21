using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrades : MonoBehaviour
{
    public int price;
    public string upgradeType;
    public string upgradeDescription;
    // remove this later
    public int currentLevel = 1;
    public int maxLevel;

    public purchaseMenu purchaseMenu;
    
    public void OnHoverEnter()
    {
        LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
    }

    public void OnHoverExit()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1 , 1), 0.1f);
    }

    public void OnClick()
    {
        purchaseMenu.gameObject.SetActive(true);
        LeanTween.scale(purchaseMenu.gameObject, new Vector3(1, 1, 1), 0.15f);

        purchaseMenu.UpdatePurchaseInfo(upgradeType, price, upgradeDescription);
    }

    public void LoadData(SaveData saveData)
    {
        foreach (var upgrade in saveData.upgradeLevels)
        {
            if (upgrade.type == upgradeType)
            {
                currentLevel = upgrade.currentLevel;
            }
        }

        price = price * (int)(Mathf.Pow(2, currentLevel - 1));

        if (currentLevel < maxLevel)
        {
            this.enabled = true;
            GetComponent<EventTrigger>().enabled = true;
        }
        else
        {
            this.enabled = false;
            GetComponent<EventTrigger>().enabled = false;
        }
    }
}
