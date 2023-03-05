using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuneComponent : MonoBehaviour
{
    [SerializeField] private TuningManager m_tuningManager;
    [SerializeField] private List<Image> m_stars;
    [SerializeField] private Sprite m_filledStarSprite;


    public void OnUIStageChange(int currentStage)
    {
      
        for (int i = 0; i < currentStage; i++)
        {
            m_stars[i].sprite = m_filledStarSprite;
        }
    }
}
