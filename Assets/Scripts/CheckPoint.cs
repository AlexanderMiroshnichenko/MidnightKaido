using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public LeadearRaceBoard leadearRaceBoard;
    

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
