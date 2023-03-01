using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsChanger : MonoBehaviour
{
    public Options options;
    public Slider sensivity;
    public Slider[] audio;
    public Slider fov;
    public Toggle isPostProcess;

    private void Awake()
    {
        SetSliderValues();
    }
    private void SetSliderValues()
    {
        sensivity.value = options.sensivity;
        audio[0].value = options.carVolume;
        audio[1].value = options.musicVolume;
        fov.value = options.fov;
        isPostProcess.isOn = options.isPostProcess;
    }
    public void OptionsChange()
    {   
        options.sensivity = sensivity.value;
        options.carVolume = audio[0].value;
        options.musicVolume = audio[1].value;
        options.fov = fov.value;
        options.isPostProcess = isPostProcess.isOn;
    }

    public void GearBoxToManual()
    {
        options.gearBoxType = Options.GearBoxType.manual;
    }
    public void GearBoxToAuto()
    {
        options.gearBoxType = Options.GearBoxType.auto;
    }
}
