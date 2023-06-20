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
    public int attackNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value -= decreaseValue * Time.deltaTime;
    }

    public void Attack()
    {
        slider.value += attackValue;
        if (slider.value >= slider.maxValue)
        {
            slider.value = 0.5f;
            attackNumber += 1;
            if (attackNumber == 3)
            {
                attackFinished.Invoke();
            }
            else 
            {
                attackLanded.Invoke();
            }
        }
    }
}
