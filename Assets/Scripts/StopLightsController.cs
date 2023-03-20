using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLightsController : MonoBehaviour
{
   [SerializeField] private GameObject m_stopLights;


    private void Awake()
    {
        TurnLightsOff();
    }
    public void TurnLightsOn()
    {
        m_stopLights.SetActive(true);
    }

    public void TurnLightsOff()
    {
        m_stopLights.SetActive(false);
    }

}
