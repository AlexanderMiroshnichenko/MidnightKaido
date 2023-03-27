using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceScoreCount : MonoBehaviour
{
    public PlayerScore playerScore;
    public BonusScoreManager bonusScoreManager;
    public TMPro.TMP_Text[] speedValues;
    public TMPro.TMP_Text[] pointValues;
    public List<int> speeds = new List<int>();
    public TMPro.TMP_Text stylePointsText;

    public TMPro.TMP_Text totalPoints;
   public void CountScore()
    {

        for(int i=0; i < speeds.Count; i++)
        {
            if (speeds[i] >= 150)
            {
                playerScore.score += 500;
                pointValues[i].text = 500.ToString();
            }
        }
        totalPoints.text = "TOTAL: "+playerScore.score.ToString();
        stylePointsText.text = bonusScoreManager.stylePoitnts.ToString();
    }

    public void UpdateUI()
    {

       /* for (int i = 0; i < speedValues.Length; i++)
        {
            if (int.Parse(speedValues[i].text) >= 150)
            {
                pointValues[i].text = 500.ToString();
            } else pointValues[i].text = 0.ToString();
        }*/
    }
}
