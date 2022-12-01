using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Engine : MonoBehaviour
{
    public float currentRpm;
    public float minRpm;
    public float maxRpm;
    public float motorTorque;
    public float forceSpeed;
    public float reduceSpeed;
    public TextMeshProUGUI rpm;

    void Start()
    {
        currentRpm = minRpm;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rpm.text = "Rpm: " + currentRpm;
        if (Input.GetAxis("Vertical") > 0)
        {
            currentRpm += forceSpeed * Time.fixedDeltaTime;
        }
        else currentRpm -= reduceSpeed * Time.fixedDeltaTime;



        if (currentRpm <= minRpm)
        {
            currentRpm = minRpm;
        }
    }
}
