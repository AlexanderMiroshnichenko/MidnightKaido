
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OptionsApplyer : MonoBehaviour
{
    public Options options;

    public AudioSource[] carAudio;
    public AudioSource music;

    public CinemachineVirtualCamera camera;

    public InputController playerController;

    public GameObject globalVolume;

    public Controller car;

    public static OptionsApplyer instance;

   

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        
    }
    private void Start()
    {
        var carAudioObjects = GameObject.FindGameObjectsWithTag("carAudio");

        if (carAudioObjects != null)
            for (int i = 0; i < carAudioObjects.Length; i++)
            {
                carAudio[i] = carAudioObjects[i].GetComponent<AudioSource>();
            }


        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<InputController>();
            car = player.GetComponentInChildren<Controller>();
        }

        globalVolume = GameObject.FindGameObjectWithTag("PostProcess");

        camera = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();

        var bgMusic = GameObject.FindAnyObjectByType<BGmusic>();

        if (bgMusic != null)
            music = bgMusic.GetComponent<AudioSource>();

        OptionsApply();
    }
    private void OnEnable()
    {
        
    }
    public void OptionsApply()
    {
        CarAudioApply();
        BGMusicApply();
        SensivityApply();
        PostProcessApply();
        GearBoxApply();
    }

    public void CarAudioApply()
    {
        foreach(AudioSource audio in carAudio)
        {
            audio.volume = options.carVolume;
        }
    }

    public void BGMusicApply()
    {
        music.volume = options.musicVolume;
    }

    public void SensivityApply()
    {
        playerController.steeringSensivity = options.sensivity;
    }
    public void PostProcessApply()
    {
        globalVolume.SetActive(options.isPostProcess);
    }

    public void GearBoxApply()
    {
        if (options.gearBoxType == Options.GearBoxType.manual)
        {
            car.gearBoxType = Controller.GearBoxType.manual;
        }
        else car.gearBoxType = Controller.GearBoxType.auto;
    }


}
