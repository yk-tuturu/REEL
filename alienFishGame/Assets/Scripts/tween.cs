using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tween : MonoBehaviour
{
    TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    // Start is called before the first frame update
    public void tweenIn()
    {
        Color initial = Color.white;
        Color fadeOut = initial;
        fadeOut.a = 0f;
    
        LeanTween.moveLocal(gameObject, new Vector3(transform.localPosition.x, transform.localPosition.y + 30, transform.localPosition.z), 1f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.value(text.gameObject, callback, fadeOut, initial, 0.5f);
        LeanTween.value(text.gameObject, callback, initial, fadeOut, 0.4f).setDelay(0.6f).setOnComplete(OnComplete);
    }

    void callback(Color val)
    {
        text.color = val;
    }

    void OnComplete()
    {
        Destroy(gameObject);
    }
}
