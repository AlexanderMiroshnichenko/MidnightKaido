using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private GameObject _gamePlayCanvas;
    [SerializeField]  private CameraStateMachine _cameraStateMachine;

    private void Awake()
    {
        StopTime();
       Application.targetFrameRate=30;
       
    }
    private void Start()
    {
        // ShowUpState();
        // StartRaceState();
    }

    private void OnEnable()
    {
        
    }


    public void ShowUpState()
    {
        _gamePlayCanvas.SetActive(false);
       
        _inputController.enabled = false;
    }
    public void StartRaceState()
    {
        _gamePlayCanvas.SetActive(true);
       
        _inputController.enabled = true;
    }
    public void LoseState()
    {
        _gamePlayCanvas.SetActive(false);
       
        //active loose canvas
    }
    public void FinishState()
    {
        _gamePlayCanvas.SetActive(false);
       
        //active end race canvas

    }

    public void StopTime()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }
    public void StartTime()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }



}
