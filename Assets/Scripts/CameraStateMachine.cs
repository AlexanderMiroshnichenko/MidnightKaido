using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine : MonoBehaviour
{
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _showUpRaceCam;






    public void ShowUpState()
    {
        _mainCam.SetActive(false);
        _showUpRaceCam.SetActive(true);
    }

    public void OnRaceState()
    {
        _mainCam.SetActive(true);
        
    }




}
