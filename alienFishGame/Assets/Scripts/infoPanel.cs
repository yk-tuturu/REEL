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

    public FMODUnity.EventReference creatureEvent;
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

        Sprite sprite = Resources.Load<Sprite>("fishPanels/fish-" + index.ToString() + "_1");

        // slight exception for the overlord
        infoPanelImage.sprite = sprite;
        
        description.text = fish.description;
        
        var fishSprite = Resources.Load<Sprite>("fishIcons/" + "fish" + index.ToString());

        if (index != 21)
        {
            fishImage.color = new Color(1, 1, 1, 1);
            fishImage.sprite = fishSprite;
        }
        else 
        {
            fishImage.color = new Color(1, 1, 1, 0);
        }

        // play audio corresponding to fish
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(creatureEvent);
        instance.setParameterByName("Fish ID", index);
        instance.start();
        instance.release();
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
