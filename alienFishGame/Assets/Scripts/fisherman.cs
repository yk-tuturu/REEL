using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fisherman : MonoBehaviour
{
    public FMODUnity.EventReference chrFishermanEvent;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                Debug.Log("over UI");
                return;
            }
            FMODUnity.RuntimeManager.PlayOneShot(chrFishermanEvent, transform.position);
        }
    }
}