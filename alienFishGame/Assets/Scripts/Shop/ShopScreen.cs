using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.15f).setOnComplete(OnComplete);
    }

    public void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
