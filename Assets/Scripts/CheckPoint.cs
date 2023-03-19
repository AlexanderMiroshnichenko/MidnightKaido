using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public LeadearRaceBoard leadearRaceBoard;
    //public bool hasPassedThis;
    /*  private void OnTriggerEnter(Collider other)
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
      }*/

    private void OnTriggerEnter(Collider other)
    {
        
            switch (other.gameObject.GetComponent<CarRaceStats>().carId)
            {
                case 0: leadearRaceBoard.carRaceStats[0].checkPointsPassed++;
                        break;
                case 1: leadearRaceBoard.carRaceStats[1].checkPointsPassed++;
                        break;
                case 2: leadearRaceBoard.carRaceStats[2].checkPointsPassed++;
                        break;
        }
    }


}
