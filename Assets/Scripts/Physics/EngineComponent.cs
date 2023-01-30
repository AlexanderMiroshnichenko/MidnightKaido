using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineComponent : MonoBehaviour
{
    [SerializeField] public float engineAcceleration;
    [SerializeField] private AnimationCurve torqueCurve;
    [SerializeField] public float startFriction = 50f;
    [SerializeField] public float frictionCoefficient = 0.02f;
    [SerializeField] public float engineInertia = 0.2f;
    [SerializeField] public float engineIdleRpm;
    [SerializeField] public float engineMaxRpm;
    [SerializeField] public float engineMul = 2f;
    [SerializeField] private Vector3 engineOrientation = Vector3.right;
    private Rigidbody rb;
    private GearBoxComponent gearBox;
    private float rpmToRad;
    private float radToRpm;
    public float maxEffectiveTorque;
    public float engineRpm;
    private float engineFriction;
    public float engineTorque;
    private float engineAngularVelocity;
    public bool test;
    public float lastValue;


    public void InitializeEngine(Rigidbody _rb, GearBoxComponent _gearBox)
    {
        rb = _rb;
        gearBox = _gearBox;
        rpmToRad = Mathf.PI * 2 / 60;
        radToRpm = 1 / rpmToRad;
        engineIdleRpm *= rpmToRad;
        engineMaxRpm *= rpmToRad;
        engineAngularVelocity = 100;
    }

    public void UpdatePhysics(float dt, float input, float loadTorque)
    {
        maxEffectiveTorque = torqueCurve.Evaluate(engineRpm) * engineMul;
        engineFriction = (engineRpm * frictionCoefficient) + startFriction;
        engineTorque = maxEffectiveTorque * input - engineFriction ;
        engineAcceleration = (engineTorque - loadTorque) / engineInertia ;
        engineAngularVelocity += engineAcceleration * dt;
        engineRpm = engineAngularVelocity * radToRpm;
        engineAngularVelocity = Mathf.Clamp(engineAngularVelocity, engineIdleRpm, engineMaxRpm);
        if (gearBox.GetGearBoxRatio() == 0)
        {
            rb.AddTorque(engineOrientation * engineTorque * 2);
        }
        lastValue = engineRpm;
        test = (lastValue > engineRpm) ? true : false;
    }

    public float GetAngularVelocity()
    {
        return engineAngularVelocity;
    }

    public float GetRpm()
    {
        return engineRpm;
    }

    public float GetTorque()
    {
        return engineTorque;
    }
}
