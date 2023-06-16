using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class fishOPediaButton : MonoBehaviour
{
    public Canvas canvas;
    public Vector3 maxScale;
    
    private Vector3 originalScale;
    public UnityEvent onClick;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        maxScale = originalScale * 1.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        LeanTween.scale(gameObject, maxScale, 0.1f);
    }

    void OnMouseExit()
    {
        LeanTween.scale(gameObject, originalScale, 0.1f);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                return;
            }
            canvas.gameObject.SetActive(true);
            onClick.Invoke();
        }
    }
}
