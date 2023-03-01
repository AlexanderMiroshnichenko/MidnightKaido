using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Options", menuName = "Scriptable Objects/Options")]

public class Options : ScriptableObject
{
    [Header("GamePlay")]

    public float sensivity;
   public  enum GearBoxType
    {
        manual,
        auto
    }
    public GearBoxType gearBoxType;

    [Header("Audio")]
    public float carVolume;
    public float musicVolume;


    [Header("Video")]

    public float fov;
    public bool isPostProcess;
}
