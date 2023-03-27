using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerScore m_playerScore;
    [SerializeField] private EngineComponent m_engineComponent;
    [SerializeField] private InputController m_inputController;
    [Header("Bonuse Values")]
    [SerializeField] private int m_bonusForPerfectShift;
    [SerializeField] private GameObject m_perfectShiftUI;
  
    [SerializeField] private int m_bonusForGasHold;
    [SerializeField] private GameObject m_gasHoldUIPoints;
    [SerializeField] private TMPro.TMP_Text m_gasHoldUIPointsText;
    public int stylePoitnts;
    [SerializeField] private TMPro.TMP_Text m_stylePointsUI;
    [SerializeField] private int currentGasHoldValue;

    private void Update()
    {
        WaitForPerfectShift();
       // GasHold();
    }



    private void GasHold()
    {
        if (m_inputController._inputs.Gameplay.AccBrakes.ReadValue<float>() > 0)
        {
            m_gasHoldUIPoints.SetActive(true);
         
            StartCoroutine(GasHoldBonusAdd(5f));
            
        }
        else 
        {

            m_gasHoldUIPoints.SetActive(false);

            AddGasHoldPoints();
            currentGasHoldValue = 0;
           
        }
    }






    private void WaitForPerfectShift()
    {
        if (m_engineComponent.engineRpm >= 8000 && m_engineComponent.engineRpm <= 9100 && m_inputController._inputs.Gameplay.ShiftUp.triggered)
        {
            Debug.Log("PerfectShift");
            m_playerScore.score += m_bonusForPerfectShift;
            stylePoitnts += m_bonusForPerfectShift;
            m_stylePointsUI.text = "STYLE POINTS: "+stylePoitnts.ToString();
            UpdateUIPerfectShift();
           
        }

    }

    private void UpdateUIPerfectShift()
    {
        m_gasHoldUIPointsText.text = currentGasHoldValue.ToString();
        m_perfectShiftUI.SetActive(true);
        StartCoroutine(DisableBonusUI(m_perfectShiftUI,1f));


    }

    
    IEnumerator DisableBonusUI(GameObject ui, float timeToDissable)
    {
       
        yield return new WaitForSeconds(timeToDissable);
        ui.SetActive(false);
        
    }

    IEnumerator GasHoldBonusAdd(float gasHoldTick)
    {

        yield return new WaitForSeconds(gasHoldTick);
        currentGasHoldValue += m_bonusForGasHold;
        m_gasHoldUIPointsText.text =currentGasHoldValue.ToString();

    }

    private void AddGasHoldPoints()
    {
        m_playerScore.score += currentGasHoldValue;
    }
}
