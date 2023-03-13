using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public LeadearRaceBoard leadearRace;
    public bool hasPassedThis;
    private void OnTriggerEnter(Collider other)
    {
        if (!hasPassedThis) { 
        other.gameObject.GetComponent<CarRaceStats>().checkPointsPassed++;
            hasPassedThis = true;
            leadearRace.CarSort();
         
        }
        else
        {
            Debug.Log("Teleport");
        }
    }
}
