using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GasolineManager : MonoBehaviour
{
    [SerializeField] private Slider _gasolineSlider;
    [SerializeField] private float decreasSpeed;

    private void Start()
    {
        _gasolineSlider.value = _gasolineSlider.maxValue;
    }
    private void FixedUpdate()
    {
        _gasolineSlider.value -= Time.fixedDeltaTime*decreasSpeed;
    }

    public void OnGasolineTrigger()
    {
        _gasolineSlider.value = _gasolineSlider.maxValue;
    }
}
