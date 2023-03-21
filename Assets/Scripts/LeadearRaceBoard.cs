using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeadearRaceBoard : MonoBehaviour
{
    public List<CarRaceStats> carRaceStats = new List<CarRaceStats>();

    public List<CarRaceStats> dynamicSortStats = new List<CarRaceStats>();

    public TMP_Text postionText;

    public Color32 color1;
    public Color32 color2;
    public Color32 color3;

    public void CarSort()
    {

        dynamicSortStats.Sort((x, y) => x.checkPointsPassed.CompareTo(y.checkPointsPassed));
        
    }
    private void Update()
    {
        CarSort();
        UpdateUI();
    }

    private void UpdateUI()
    {
       foreach(var c in dynamicSortStats)
        {
            if (c.carId == 0)
            {
                var position = (dynamicSortStats.Count - dynamicSortStats.IndexOf(c));
                postionText.text = position.ToString();
                switch (position)
                {
                    case 3: postionText.color = color3; break;
                    case 2: postionText.color = color2; break;
                    case 1: postionText.color = color1; break;
                }
            }
        }

      

    }
}
