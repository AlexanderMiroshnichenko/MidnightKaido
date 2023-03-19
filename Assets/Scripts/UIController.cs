using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_closedOnAwake;
    [SerializeField] private GameObject[] m_openedOnAwake;
  

    private GameObject m_previousStage;

    private void Awake()
    {
        foreach(var g in m_closedOnAwake)
        {
            g.SetActive(false);
        }

        foreach (var g in m_openedOnAwake)
        {
            g.SetActive(true);
        }
    }

   
    public void ToPreviusStage()
    {

    }
}
