using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedTrapTrigger : MonoBehaviour
{
    public SetGetPhoto setGetPhoto;
    public TMP_Text speedText;
    public AudioSource photoSound;
    public GameObject speedReaction;
    public TMP_Text reactionSpeedText;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            speedReaction.SetActive(true);
          
            setGetPhoto.OnSpeedTrap();
            photoSound.Play();
           var dashboard = other.GetComponentInChildren<Dashboard>();
            speedText.text = "Speed: "+Mathf.RoundToInt(dashboard.speed).ToString()+" km/h";
            reactionSpeedText.text = "YOUR SPEED IS " + Mathf.RoundToInt(dashboard.speed).ToString() + " km/h";
        }
      
    }
}
