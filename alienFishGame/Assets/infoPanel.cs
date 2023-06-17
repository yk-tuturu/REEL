using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infoPanel : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI length;
    public TextMeshProUGUI description;
    public Transform starContainer;
    public Image fishImage;
    public GameObject star;
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
        name.text = fish.name;
        weight.text = "Weight: " + fish.weight;
        length.text = "Length: " + fish.length;
        description.text = fish.description;
        
        fishImage.sprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());

        foreach (Transform child in starContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < fish.rarity; i++)
        {
            Instantiate(star, new Vector3(0,0,0), Quaternion.identity, starContainer);
        }
    }
}
