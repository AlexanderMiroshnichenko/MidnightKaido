using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private GameObject _gamePlayCanvas;
    [SerializeField]  private CameraStateMachine _cameraStateMachine;

    private void Start()
    {
        ShowUpState();
        StartRaceState();
    }
    public void ShowUpState()
    {
        _gamePlayCanvas.SetActive(false);
        _cameraStateMachine.ShowUpState();
        _inputController.enabled = false;
    }
    public void StartRaceState()
    {
        _gamePlayCanvas.SetActive(true);
        _cameraStateMachine.OnRaceState();
        _inputController.enabled = true;
    }
    public void LoseState()
    {
        _gamePlayCanvas.SetActive(false);
        _cameraStateMachine.ShowUpState();
        //active loose canvas
    }
    public void FinishState()
    {
        _gamePlayCanvas.SetActive(false);
        _cameraStateMachine.ShowUpState();
        //active end race canvas

    }

   

}