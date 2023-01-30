using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Tune",menuName = "Scriptable Objects/CarTune")]
public class CarTune : ScriptableObject
{
    [Header("Suspension")]
    public float frontRestLength;
    public float frontSuspensionStiffness;
    public float frontDamperStiffnes;
    public float frontCamber;
    public float rearRestLength;
    public float rearSuspensionStiffness;
    public float rearDamperStiffnes;
    public float rearCamber;
    public float turnRadius;

    [Header("Wheels")]
    public float radius;
    public float mass;
    public float frontGrip;
    public float rearGrip;

    [Header("Engine")]
    public float startFriction;
    public float frictionCoefficient;
    public float engineInertia;
    public float idleRPM;
    public float maxRPM;
    public float engineTorqueCoeff;

    [Header("GearBox")]
    public float[] gearRatios;
    public float shiftTime;
    


    [Header("Differential")]
    public bool isLocked;
    public float differentialRatio;


}
