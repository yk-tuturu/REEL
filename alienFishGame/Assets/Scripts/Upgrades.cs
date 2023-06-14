using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int price; 
    public string upgradeType;
    public string upgradeDescription;
    public int nextLevel = 2;
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
}
