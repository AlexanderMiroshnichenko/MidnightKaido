using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSounds : MonoBehaviour
{
    public EngineComponent engine;

    public AudioClip idle;
    public AudioClip lowOff;
    public AudioClip lowOn;
    public AudioClip midOff;
    public AudioClip midOn;
    public AudioClip highOff;
    public AudioClip highOn;
    public AudioClip maxRPM;

    public AnimationCurve idleCurve;
    public AnimationCurve lowOffCurve;
    public AnimationCurve lowOnCurve;
    public AnimationCurve midOffCurve;
    public AnimationCurve midOnCurve;
    public AnimationCurve highOffCurve;
    public AnimationCurve highOnCurve;
    public AnimationCurve maxRPMCurve;


    public AudioSource idleSource;
    public AudioSource lowOffSource;
    public AudioSource lowOnSource;
    public AudioSource midOffSource;
    public AudioSource midOnSource;
    public AudioSource highOffSource;
    public AudioSource highOnSource;
    public AudioSource maxRPMSource;

    private bool isAccelerated;


    private void Start()
    {
        idleSource=SetUpEngineAudioSource(idle);
        lowOffSource = SetUpEngineAudioSource(lowOff);
        lowOnSource = SetUpEngineAudioSource(lowOn);
        midOffSource = SetUpEngineAudioSource(midOff);
        midOnSource = SetUpEngineAudioSource(midOn);
        highOffSource = SetUpEngineAudioSource(highOff);
        highOnSource = SetUpEngineAudioSource(highOn);
        maxRPMSource = SetUpEngineAudioSource(maxRPM);
    }
    private void FixedUpdate()
    {
        if (engine.engineTorque > 0)
            isAccelerated = true;
        else isAccelerated = false;

        idleSource.volume = idleCurve.Evaluate(engine.engineRpm);
        // idleSource.volume = idleCurve.Evaluate(engine.engineRpm);
        lowOnSource.volume = lowOnCurve.Evaluate(engine.engineRpm);
        lowOnSource.pitch = lowOnCurve.Evaluate(engine.engineRpm);
        // idleSource.volume = idleCurve.Evaluate(engine.engineRpm);
        midOnSource.volume = midOnCurve.Evaluate(engine.engineRpm);
        //  idleSource.volume = idleCurve.Evaluate(engine.engineRpm);
        highOnSource.volume = highOnCurve.Evaluate(engine.engineRpm);
        maxRPMSource.volume = maxRPMCurve.Evaluate(engine.engineRpm);








    }
    private AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.loop = true;
        source.Play();
        return source;
    }

}
