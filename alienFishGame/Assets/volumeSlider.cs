using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{
    public string busPath;
    public Slider slider;

    private FMOD.Studio.Bus bus;

    // Start is called before the first frame update
    void Start()
    {
        bus = FMODUnity.RuntimeManager.GetBus(busPath);

        bus.getVolume(out float volume);
        slider.value = volume * slider.maxValue;
    }

    public void UpdateSliderValue()
    {
        bus.setVolume(slider.value /slider.maxValue);
    }
}
