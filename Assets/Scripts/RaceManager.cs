using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RaceManager : MonoBehaviour
{
    [SerializeField] private GamePlayController m_gamePlayController;
    [SerializeField] private float m_startTimerValue;

    [SerializeField] private float m_timerCurrentValue;
    [SerializeField] private bool m_isTimerOn;
    [SerializeField] private InputController[] aiInputs;
    [SerializeField] private InputController playerInput;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTextShowTime;
    [SerializeField] private GameObject startText;


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
        timerText.enabled = true;
        
    }
    public void FinishTimer()
    {
        m_isTimerOn = false;
        timerText.enabled = false;
       
    }

    private void Update()
    {
       
        if (m_isTimerOn)
        {
            m_startTimerValue -= Time.unscaledDeltaTime;

          
            timerText.text = (Mathf.Round(m_startTimerValue)+1).ToString();

            if (m_startTimerValue <= 0)
            {
              
               
                FinishTimer();
                StartAI();
                playerInput.enabled = true;
                startText.SetActive(true);


            }
            

        }
    }
    

}
