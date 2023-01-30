using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour{

   
    
    private GameObject audioObject;
    public EngineComponent engine;

    [Range(0,1)]public float startOffValue = 0.35f;

    public float Load;
    public float loadLerpSpeed = 15;

    public AudioClip lowAccelClip;                                              
    public AudioClip lowDecelClip;                                              
    public AudioClip highAccelClip;                                             
    public AudioClip highDecelClip; 
    [Header("Tubro Sound")]
    public AudioClip Turbo;   
    [Range(0,2)]public float turboVolume;  


    [Header("Pitch")]
    [Range(0.5f,1)]public float Pitch = 1f;                              
    public float lowPitchMin = 1f;                                              
    public float lowPitchMax = 6f;                                              
    [Range(0,1)]public float highPitchMultiplier = 0.25f;       
    [Range(0,1)]public float pitchMultiplier = 1f;                              
   

    private float accFade = 0;
    private float acceleration;
    private float maxRolloffDistance = 500;                                      
    private AudioSource m_LowAccel; 
    private AudioSource m_LowDecel; 
    private AudioSource m_HighAccel;
    private AudioSource m_HighDecel;
    private AudioSource m_Turbo;


    private void Start(){


        m_HighAccel = SetUpEngineAudioSource(highAccelClip);
        m_LowAccel = SetUpEngineAudioSource(lowAccelClip);
        m_LowDecel = SetUpEngineAudioSource(lowDecelClip);
        m_HighDecel = SetUpEngineAudioSource(highDecelClip);
        if(Turbo != null)m_Turbo = SetUpEngineAudioSource(Turbo);


      
      

       // lowPitchMax = (engine.engineMaxRpm / 1000) / 2;
        
    }

  


    private void FixedUpdate(){
                    
        accFade = Mathf.Lerp(accFade,Mathf.Abs( acceleration ), loadLerpSpeed * Time.deltaTime );

            if(engine.engineTorque > 0 &&!engine.test)
                acceleration = 1;
            else acceleration = 0;

            float pitch = ULerp(lowPitchMin, lowPitchMax, engine.engineRpm / engine.engineMaxRpm);
            pitch = Mathf.Min(lowPitchMax, pitch);
            m_LowAccel.pitch = pitch*pitchMultiplier;
            m_LowDecel.pitch = pitch*pitchMultiplier;
            m_HighAccel.pitch = pitch*highPitchMultiplier*pitchMultiplier;
            m_HighDecel.pitch = pitch*highPitchMultiplier*pitchMultiplier;

            float decFade = 1 - accFade;
            float highFade = Mathf.InverseLerp(0.2f, 0.8f,  engine.engineRpm / engine.engineMaxRpm);
            float lowFade = 1 - highFade;
            
            highFade = 1 - ((1 - highFade)*(1 - highFade));
            lowFade = 1 - ((1 - lowFade)*(1 - lowFade));
            //accFade = 1 - ((1 - accFade)*(1 - accFade));
            decFade = 1 - ((1 - decFade)*(1 - decFade));
            m_LowAccel.volume = lowFade*accFade;
            m_LowDecel.volume = lowFade*decFade;
            m_HighAccel.volume = highFade*accFade;
            m_HighDecel.volume = highFade*decFade;
                
            
       
    }

    private AudioSource SetUpEngineAudioSource(AudioClip clip){
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.spatialBlend = 1;
        source.loop = true;
        source.dopplerLevel = 0;
        source.time = Random.Range(0f, clip.length);
        source.Play();
        source.minDistance = 5;
        source.maxDistance = maxRolloffDistance;
        return source;
    }

    private  float ULerp(float from, float to, float value){
        return (1.0f - value)*from + value*to;
    }

}

