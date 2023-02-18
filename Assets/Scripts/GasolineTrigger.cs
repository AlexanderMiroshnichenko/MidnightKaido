using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolineTrigger : MonoBehaviour
{
    public GasolineManager _gasolineManager;

    private void Awake()
    {
        _gasolineManager = FindObjectOfType<GasolineManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _gasolineManager.OnGasolineTrigger();
        }
    }
   
}
