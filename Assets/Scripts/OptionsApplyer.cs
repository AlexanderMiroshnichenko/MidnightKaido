
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
       
       SceneManager.sceneLoaded += OnSceneLoaded;

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

        
    }
  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
         

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
    public void OptionsApply()
    {
      
        BGMusicApply();
        SensivityApply();
        PostProcessApply();
        GearBoxApply();
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
