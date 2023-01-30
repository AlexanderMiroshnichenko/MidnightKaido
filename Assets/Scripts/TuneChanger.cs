using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuneChanger : MonoBehaviour
{
    public CarTune carTune;
    public CarTune carTuneDefault;
    public Slider[] sliderSuspension;
    public Slider[] sliderWheels;
    public Slider[] sliderEngine;
    public Slider[] sliderGearBox;
    public Toggle toggleDifferential;
    public Slider[] sliderDifferential;

    private void Awake()
    {
        SetSliderValues();
    }
    public void TuneChange()
    {
        SuspensionChange();
        WheelsChange();
        EngineChange();
        GearBoxChange();
        DifferentialChange();
    }

    public void TuneToDefault()
    {
        carTune.frontRestLength = carTuneDefault.frontRestLength;
        carTune.frontSuspensionStiffness = carTuneDefault.frontSuspensionStiffness;
        carTune.frontDamperStiffnes = carTuneDefault.frontDamperStiffnes;
        carTune.frontCamber = carTuneDefault.frontCamber;
        carTune.rearRestLength = carTuneDefault.rearRestLength;
        carTune.rearSuspensionStiffness = carTuneDefault.rearSuspensionStiffness;
        carTune.rearDamperStiffnes = carTuneDefault.rearDamperStiffnes;
        carTune.rearCamber = carTuneDefault.rearCamber;
        carTune.turnRadius = carTuneDefault.turnRadius;

        carTune.radius = carTuneDefault.radius;
        carTune.mass = carTuneDefault.mass;

        carTune.startFriction = carTuneDefault.startFriction;
        carTune.frictionCoefficient = carTuneDefault.frictionCoefficient;
        carTune.engineInertia = carTuneDefault.engineInertia;
        carTune.idleRPM = carTuneDefault.idleRPM;
        carTune.maxRPM = carTuneDefault.maxRPM;
        carTune.engineTorqueCoeff = carTuneDefault.engineTorqueCoeff;


        for (int i = 0; i < sliderGearBox.Length - 1; i++)
        {
            carTune.gearRatios[i] = carTuneDefault.gearRatios[i];
        }

        carTune.shiftTime = carTuneDefault.shiftTime;

        carTune.isLocked = carTuneDefault.isLocked;
        carTune.differentialRatio = carTuneDefault.differentialRatio;
        SetSliderValues();
    }


    private void SuspensionChange()
    {
        carTune.frontRestLength = sliderSuspension[0].value;
        carTune.frontSuspensionStiffness = sliderSuspension[1].value;
        carTune.frontDamperStiffnes = sliderSuspension[2].value;
        carTune.frontCamber = sliderSuspension[3].value;
        carTune.rearRestLength = sliderSuspension[4].value;
        carTune.rearSuspensionStiffness = sliderSuspension[5].value;
        carTune.rearDamperStiffnes = sliderSuspension[6].value;
        carTune.rearCamber = sliderSuspension[7].value;
        carTune.turnRadius = sliderSuspension[8].value;
    }

    private void SetSliderValues()
    {
        sliderSuspension[0].value = carTune.frontRestLength;
        sliderSuspension[1].value = carTune.frontSuspensionStiffness;
        sliderSuspension[2].value = carTune.frontDamperStiffnes;
        sliderSuspension[3].value = carTune.frontCamber;
        sliderSuspension[4].value = carTune.rearRestLength;
        sliderSuspension[5].value = carTune.rearSuspensionStiffness;
        sliderSuspension[6].value = carTune.rearDamperStiffnes;
        sliderSuspension[7].value = carTune.rearCamber;
        sliderSuspension[8].value = carTune.turnRadius;

        sliderWheels[0].value = carTune.radius;
        sliderWheels[1].value = carTune.mass;

        sliderEngine[0].value = carTune.startFriction;
        sliderEngine[1].value = carTune.frictionCoefficient;
        sliderEngine[2].value = carTune.engineInertia;
        sliderEngine[3].value = carTune.idleRPM;
        sliderEngine[4].value = carTune.maxRPM;
        sliderEngine[5].value = carTune.engineTorqueCoeff;

        for (int i = 0; i < sliderGearBox.Length - 1; i++)
        {
            sliderGearBox[i].value = carTune.gearRatios[i];
        }
        sliderGearBox[6].value = carTune.shiftTime;

        toggleDifferential.isOn = carTune.isLocked;
        sliderDifferential[0].value = carTune.differentialRatio;
    }
    private void WheelsChange()
    {
        carTune.radius = sliderWheels[0].value;
        carTune.mass = sliderWheels[1].value;
    }

    private void EngineChange()
    {
        carTune.startFriction = sliderEngine[0].value;
        carTune.frictionCoefficient = sliderEngine[1].value;
        carTune.engineInertia = sliderEngine[2].value;
        carTune.idleRPM = sliderEngine[3].value;
        carTune.maxRPM = sliderEngine[4].value;
        carTune.engineTorqueCoeff = sliderEngine[5].value;
    }

    private void GearBoxChange()
    {
       for(int i = 0; i < sliderGearBox.Length-1; i++)
        {
            carTune.gearRatios[i] = sliderGearBox[i].value;
        }

        carTune.shiftTime = sliderGearBox[6].value;
    }
    private void DifferentialChange()
    {
        carTune.isLocked = toggleDifferential.isOn;
        carTune.differentialRatio = sliderDifferential[0].value;
    }
}