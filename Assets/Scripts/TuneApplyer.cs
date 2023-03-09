using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneApplyer : MonoBehaviour
{
    [SerializeField] private CarTune carTune;
    [SerializeField] private GearBoxComponent gearBox;
    [SerializeField] private DifferentialComponent differential;
    [SerializeField] private WheelControllerTFM[] frontWheels;
    [SerializeField] private WheelControllerTFM[] rearWheels;
    [SerializeField] private EngineComponent engine;

    private void Awake()
    {
        TuneApply();
    }

    public void TuneApply()
    {
        ApplyEngine();
        ApplySuspension();
        ApplyWheels();

        ApplyGearBox();
        ApplyDifferential();
    }


    private void ApplySuspension()
    {
        foreach (WheelControllerTFM w in frontWheels)
        {
            w.restLength = carTune.frontRestLength;
            w.suspensionStiffness = carTune.frontSuspensionStiffness;
            w.damperStiffness = carTune.frontDamperStiffnes;
            w.longCoeff = carTune.frontGrip;
            w.latCoeff = carTune.frontGrip;

        }
        foreach (WheelControllerTFM w in rearWheels)
        {
            w.restLength = carTune.rearRestLength;
            w.suspensionStiffness = carTune.rearSuspensionStiffness;
            w.damperStiffness = carTune.rearDamperStiffnes;
            w.longCoeff = carTune.rearGrip;
            w.latCoeff = carTune.rearGrip;
        }
        frontWheels[0].camber = -carTune.frontCamber;
        rearWheels[0].camber = -carTune.rearCamber;
        frontWheels[1].camber = carTune.frontCamber;
        rearWheels[1].camber = carTune.rearCamber;
    }

    private void ApplyWheels()
    {
        foreach (WheelControllerTFM w in frontWheels)
        {
            w.wheelMass = carTune.mass;
            w.wheelRadius = carTune.radius;
        }
        foreach (WheelControllerTFM w in rearWheels)
        {
            w.wheelMass = carTune.mass;
            w.wheelRadius = carTune.radius;
        }
    }

    private void ApplyEngine()
    {
        engine.startFriction = carTune.startFriction;
        engine.frictionCoefficient = carTune.frictionCoefficient;
        engine.engineIdleRpm = carTune.idleRPM;
        engine.engineInertia = carTune.engineInertia;
        engine.engineMaxRpm = carTune.maxRPM;
        engine.engineMul = carTune.engineTorqueCoeff;

    }

    private void ApplyGearBox()
    {
        gearBox.gearboxRatio[0] = carTune.gearRatios[0];
        gearBox.gearboxRatio[1] = 0f;
        gearBox.gearboxRatio[2] = carTune.gearRatios[1];
        gearBox.gearboxRatio[3] = carTune.gearRatios[2];
        gearBox.gearboxRatio[4] = carTune.gearRatios[3];
        gearBox.gearboxRatio[5] = carTune.gearRatios[4];
        gearBox.gearboxRatio[6] = carTune.gearRatios[5];
        gearBox.shiftTime = carTune.shiftTime;
    }
    private void ApplyDifferential()
    {
        differential.differentialRatio = carTune.differentialRatio;
        differential.diffLocked = carTune.isLocked;
    }

}