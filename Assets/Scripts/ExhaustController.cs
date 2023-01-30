using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustController : MonoBehaviour
{
    public AudioSource exhaustShot;
    public Animator animExh;
    public ParticleSystem exhaustShotVFX;
    public EngineComponent engine;
    public Controller controller;
    public float lastInputValue;
    public float timeBetweenShots;
    public bool isAlreadyShoted=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (engine.engineTorque<0&&controller.inputThrottle>0)
        {
            
            PlayExhaustOnDeAcc();
        }

        if (engine.engineRpm >= 9400)
        {
            PlayExhaustOnMaxRPM();
            
        } else animExh.SetBool("isOnMaxRPM", false);

    }

    private void PlayExhaustOnMaxRPM()
    {

        animExh.SetBool("isOnMaxRPM",true);
         
    }
    private void PlayExhaustOnDeAcc()
    {
        animExh.SetTrigger("deAcc");
       
    }

    public void ShotExhaust()
    {
        exhaustShot.Play();
        exhaustShotVFX.Play();
    }
   
}
