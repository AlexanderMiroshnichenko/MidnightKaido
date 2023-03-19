using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadearRaceBoard : MonoBehaviour
{
    public List<CarRaceStats> carRaceStats = new List<CarRaceStats>();
 
  
    

  

    public void CarSort()
    {
        carRaceStats.Sort();
    }
}
