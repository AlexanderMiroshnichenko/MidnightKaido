using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrapAttention : MonoBehaviour
{
    public GameObject speedTrapAttention;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            speedTrapAttention.SetActive(true);
    }
}
