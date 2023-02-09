using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float inputThrottle;
    public float inputBrakes;
    public float inputSteering;
    public float inputHandBrake;

    public InputManager _inputs;

    [SerializeField] private float _steeringSensivity;

   

    private void Awake()
    {
        _inputs = new InputManager();    
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    void Update()
    {
        inputThrottle = _inputs.Gameplay.AccBrakes.ReadValue<float>() < 0 ? 0 : _inputs.Gameplay.AccBrakes.ReadValue<float>();
        inputBrakes = _inputs.Gameplay.AccBrakes.ReadValue<float>() > 0 ? 0 : _inputs.Gameplay.AccBrakes.ReadValue<float>();
        inputSteering = Mathf.Lerp(inputSteering, _inputs.Gameplay.Steering.ReadValue<float>(), _steeringSensivity*Time.deltaTime);
        
        inputHandBrake = _inputs.Gameplay.HandBrake.ReadValue<float>();
    }
}
