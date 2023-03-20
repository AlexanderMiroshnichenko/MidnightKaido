using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Finish : MonoBehaviour
{
    public bool isRaceOver=false;
    public PlayableDirector finishTimeLine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarRaceStats>().enabled==true&&isRaceOver==false)
        {
            finishTimeLine.Play();
            isRaceOver = true;
            other.gameObject.GetComponent<CarRaceStats>().hasWon = true;
        }
     
    }
}
