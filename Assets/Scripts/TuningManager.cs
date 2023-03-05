using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuningManager : MonoBehaviour
{
    [SerializeField] private CarTune m_carTune;
    [SerializeField] private TuneApplyer m_tuneApplyer;
    [SerializeField] private int maxTurboStage;
    [SerializeField] private int maxEngineStage;
    [SerializeField] private int maxSuspensionStage;
    [SerializeField] private int maxGearBoxStage;
    [SerializeField] private int maxWheelStage;
    [SerializeField] private int turboStage;
    [SerializeField] private int engineStage;
    [SerializeField] private int suspensionStage;
    [SerializeField] private int gearBoxStage;
    [SerializeField] private int wheelStage;
    

    [SerializeField] private List<TuneComponent> tuneComponents;


    private void Awake()
    {
        turboStage = m_carTune.turboStage;
        engineStage = m_carTune.engineStage;
        suspensionStage = m_carTune.suspensionStage;
        gearBoxStage = m_carTune.gearBoxStage;
        wheelStage = m_carTune.wheelsStage;
        tuneComponents[0].OnUIStageChange(turboStage);
        tuneComponents[1].OnUIStageChange(engineStage);
        tuneComponents[2].OnUIStageChange(suspensionStage);
        tuneComponents[3].OnUIStageChange(gearBoxStage);
        tuneComponents[4].OnUIStageChange(wheelStage);
    }


    public void TurboToNextStage(float torqueCoeffIncrease)
    {
       
        // ToNextStage(turboStage, maxTurboStage);
        if (turboStage < maxTurboStage)
        {

            turboStage++;
            m_carTune.engineTorqueCoeff += torqueCoeffIncrease;
            m_tuneApplyer.TuneApply();
            m_carTune.turboStage = turboStage;
        }
        tuneComponents[0].OnUIStageChange(turboStage);
    }

    public void EngineToNextStage(float torqueCoeffIncrease)
    {
        // ToNextStage(engineStage, maxEngineStage);
        if (engineStage < maxEngineStage)
        {

            engineStage++;
            m_carTune.engineTorqueCoeff += torqueCoeffIncrease;
            m_tuneApplyer.TuneApply();
            m_carTune.engineStage = engineStage;
        }
        tuneComponents[1].OnUIStageChange(engineStage);
    }

    public void SuspensionToNextStage(float stiffnessIncrease)
    {
        //  ToNextStage(suspensionStage, maxSuspensionStage);
        if (suspensionStage < maxSuspensionStage)
        {

            suspensionStage++;
            m_carTune.frontSuspensionStiffness += stiffnessIncrease;
            m_carTune.rearSuspensionStiffness += stiffnessIncrease;
            m_carTune.frontDamperStiffnes += 1000f;
            m_carTune.rearDamperStiffnes += 1000f;
            m_carTune.frontRestLength -= 0.025f;
            m_carTune.rearRestLength -= 0.025f;
            m_carTune.frontCamber -= 2;
            m_tuneApplyer.TuneApply();
            m_carTune.suspensionStage=suspensionStage;

        }
        tuneComponents[2].OnUIStageChange(suspensionStage);
    }

    public void GearBoxToNextStage(float gearLengthIncrease)
    {
        // ToNextStage(gearBoxStage, maxGearBoxStage);
        if (gearBoxStage < maxGearBoxStage)
        {

            gearBoxStage++;
            for(int i = 1; i < m_carTune.gearRatios.Length; i++)
            {
                m_carTune.gearRatios[i] -= gearLengthIncrease/i;
            }
            m_tuneApplyer.TuneApply();
            m_carTune.gearBoxStage=gearBoxStage;
        }
        tuneComponents[3].OnUIStageChange(gearBoxStage);
    }

    public void WheelsToNextStage(float tireCoeffIncrease)
    {
        //  ToNextStage(wheelStage, maxWheelStage);
        if (wheelStage < maxWheelStage)
        {

            wheelStage++;
            
            m_carTune.rearGrip += tireCoeffIncrease;
            m_tuneApplyer.TuneApply();
            m_carTune.wheelsStage=wheelStage;
        }
        tuneComponents[4].OnUIStageChange(wheelStage);
    }

    public void ToNextStage(int value,int maxValue)
    {
        if (value < maxValue)
        {
           
            value++;
        }
    }


}
