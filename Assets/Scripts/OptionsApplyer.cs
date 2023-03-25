
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Audio;

public class OptionsApplyer : MonoBehaviour
{
    public Options options;

    public AudioMixer mixer;

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
        OptionsApply();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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

       

        OptionsApply();
    }
    public void OptionsApply()
    {
      
        BGMusicApply();
        SensivityApply();
        PostProcessApply();
        GearBoxApply();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void BGMusicApply()
    {
        mixer.SetFloat("MusicMaster", Mathf.Log10(options.musicVolume) * 20);
    }

    public void SensivityApply()
    {
        if(playerController!=null)
        playerController.steeringSensivity = options.sensivity;
    }
    public void PostProcessApply()
    {
        if(globalVolume!=null)
        globalVolume.SetActive(options.isPostProcess);
    }
    
    public void GearBoxApply()
    {
        if (car != null)
        {
            if (options.gearBoxType == Options.GearBoxType.manual)
            {
                car.gearBoxType = Controller.GearBoxType.manual;
            }
            else car.gearBoxType = Controller.GearBoxType.auto;

        }
        
    }


}
