using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsChanger : MonoBehaviour
{
    public Options options;
    public Slider sensivity;
    public Slider[] audioSlider;
   
    public Toggle isPostProcess;

    private void Awake()
    {
        SetSliderValues();
    }
    private void SetSliderValues()
    {
        sensivity.value = options.sensivity;
        
        audioSlider[0].value = options.musicVolume;
       
        isPostProcess.isOn = options.isPostProcess;
    }
    public void OptionsChange()
    {   
        options.sensivity = sensivity.value;
        
        options.musicVolume = audioSlider[0].value;
      
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
