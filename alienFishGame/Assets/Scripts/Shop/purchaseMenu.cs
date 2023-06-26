using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class purchaseMenu : MonoBehaviour
{
    public string type;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public int price;

    public Transform fishingRodParent;
    public Transform upgradeParent;

    public UnityEvent bossTransition;
    public GameObject soldOverlay;

    public FMODUnity.EventReference rodPurchaseEvent;
    public FMODUnity.EventReference trapPurchaseEvent;
    public FMODUnity.EventReference baitPurchaseEvent;
    public FMODUnity.EventReference marketingPurchaseEvent;
    public FMODUnity.EventReference uiCloseEvent;
    public FMODUnity.EventReference uiDeniedEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePurchaseInfo(string updateType, int updatePrice, string description)
    {
        type = updateType;
        price = updatePrice;
        descriptionText.text = description;
        priceText.text = price.ToString();
    }

    public void Close()
    {
        FMODUnity.RuntimeManager.PlayOneShot(uiCloseEvent);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);
    }

    public void Purchase()
    {
        // do nothing if not enough money
        if (FishDataManager.instance.money < price)
        {
            FMODUnity.RuntimeManager.PlayOneShot(uiDeniedEvent);
            return;
        }
        
        // I am aware of how horrible this is but pls bear with me
        // unfortunately i cant think of any better way to do this
        // maybe will change this to a switch statement later
        if (type == "rod")
        {
            FMODUnity.RuntimeManager.PlayOneShot(rodPurchaseEvent);
            RodStatManager.instance.SetRodLevel(RodStatManager.instance.rodLevel + 1);
        }
        else if (type == "bait")
        {
            FMODUnity.RuntimeManager.PlayOneShot(baitPurchaseEvent);
            RodStatManager.instance.SetBaitLevel(RodStatManager.instance.baitLevel + 1);
            TrapStatManager.instance.SetBaitLevel(TrapStatManager.instance.baitLevel + 1);
        }
        else if (type == "trap")
        {
            FMODUnity.RuntimeManager.PlayOneShot(trapPurchaseEvent);
            TrapStatManager.instance.SetTrapLevel(TrapStatManager.instance.trapLevel + 1);
        }
        else if (type == "sales")
        {
            FMODUnity.RuntimeManager.PlayOneShot(marketingPurchaseEvent);
            FishDataManager.instance.UpgradeSales();
        }
        else if (type == "extra rod")
        {
            FMODUnity.RuntimeManager.PlayOneShot(rodPurchaseEvent);
            foreach(Transform child in fishingRodParent)
            {
                FishingPole rod = child.GetComponent<FishingPole>();
                if (rod.type == "rod")
                {
                    child.gameObject.SetActive(true);
                    rod.panel.SetActive(true);
                }
            }
        }
        else if (type == "extra trap")
        {
            FMODUnity.RuntimeManager.PlayOneShot(trapPurchaseEvent);
            foreach(Transform child in fishingRodParent)
            {
                FishingPole rod = child.GetComponent<FishingPole>();
                if (rod.type == "trap")
                {
                    child.gameObject.SetActive(true);
                    rod.panel.SetActive(true);
                }
            }
        }

        // closes menu
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);

        // refreshes stats
        foreach(Transform child in fishingRodParent)
        {
            FishingPole rod = child.GetComponent<FishingPole>();
            rod.fetchStats();
        }

        // finds the correct upgrade instance and updates its data
        foreach(Transform child in upgradeParent)
        {
            Upgrades upgrade = child.GetComponent<Upgrades>();
            if (type == upgrade.upgradeType)
            {
                // spends money and increases price
                FishDataManager.instance.SpendMoney(price);
                upgrade.price = price * upgrade.multiplier;

                upgrade.currentLevel += 1;

                // if max level reached
                if (upgrade.currentLevel >= upgrade.maxLevel)
                {
                    upgrade.enabled = false;
                    child.GetComponent<EventTrigger>().enabled = false;
                    upgrade.soldOverlay.SetActive(true);
                }

                if (type == "bait" && upgrade.currentLevel == 4)
                {
                    bossTransition.Invoke();
                }
            }
        }
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
