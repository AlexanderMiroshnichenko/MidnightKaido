using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private GamePlayController m_gamePlayController;
    [SerializeField] private float m_startTimerValue;
    [SerializeField] private float m_timerSpeed;
    [SerializeField] private float m_timerCurrentValue;
    [SerializeField] private bool m_isTimerOn;
    [SerializeField] private InputController[] aiInputs;
    [SerializeField] private InputController playerInput;


  
    private void Start()
    {
       
    }

    private void StartAI()
    {
        foreach(InputController ai in aiInputs)
        {
            ai.AIThrottle = 1f;
        }
    }
    public void StartTimer()
    {
        m_isTimerOn=true;
        m_gamePlayController.StartTime();
        playerInput.enabled = false;
    }
    public void FinishTimer()
    {
        m_isTimerOn = false;
    }

    private void Update()
    {
       
        if (m_isTimerOn)
        {
            m_startTimerValue -= Time.unscaledDeltaTime;
           

            if (m_startTimerValue <= 0)
            {
                FinishTimer();
                StartAI();
                playerInput.enabled = true;
            }




        }
    }
    

}
