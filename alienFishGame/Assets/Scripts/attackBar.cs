using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class attackBar : MonoBehaviour
{
    public Slider slider;
    public float decreaseValue;
    public float attackValue;
    public UnityEvent attackLanded;
    public UnityEvent attackFinished;
    public UnityEvent attackFailed;
    public int attackNumber = 0;
    public GameObject attackPanel;

    public bool attackHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackHit)
        {
            slider.value -= decreaseValue * Time.deltaTime;

            if (slider.value <= 0)
            {
                AttackFailed();
            }
        }
    }

    public void Attack()
    {
        if (attackHit != true)
        {
            slider.value += attackValue;

            if (slider.value >= slider.maxValue)
            {
                attackHit = true;
                iTween.ShakePosition(attackPanel, iTween.Hash("amount", new Vector3(22, 22, 22), "oncomplete", "OnComplete", "oncompletetarget", this.gameObject, "time", 0.25f));
            }
        }
    }

    public void OnComplete()
    {
        slider.value = 0.5f;
        decreaseValue = decreaseValue * 1.15f;
        attackNumber += 1;
        if (attackNumber == 5)
        {
            attackFinished.Invoke();
        }
        else 
        {
            attackLanded.Invoke();
        }

        attackHit = false;
    }

    public void AttackFailed()
    {
        attackHit = true;
        iTween.ShakePosition(attackPanel, iTween.Hash("amount", new Vector3(22, 22, 22), "oncomplete", "OnCompleteFail", "oncompletetarget", this.gameObject, "time", 0.25f));
    }

    public void OnCompleteFail()
    {
        slider.value = 0.5f;
        attackFailed.Invoke();
        attackHit = false;
    }
}
