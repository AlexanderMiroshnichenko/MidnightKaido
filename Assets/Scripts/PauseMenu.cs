using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
