using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearBox : MonoBehaviour
{

    public int currentGearNumber=0;
    public int ammountOfGears;
    public TextMeshProUGUI gear;
    [SerializeField] public Engine engine;
    [SerializeField] public Wheel[] wheel;
    public bool alreadySwitched;

    void Start()
    {
        alreadySwitched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShiftUp();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShiftDown();
        }
        gear.text = currentGearNumber.ToString();

        switch (currentGearNumber)
        {
            case -1:
                foreach (Wheel w in wheel)
                {
                    engine.forceSpeed = 1000; w.motorTorque = -w.motorTorque;
                }
                break;
            case 0:
                foreach (Wheel w in wheel)
                {
                    engine.forceSpeed = 1000; w.motorTorque =0;
                }
                break;
            case 1: engine.forceSpeed = 1000;if (!alreadySwitched)
                {
                    engine.currentRpm -= 1000;
                    alreadySwitched = true;
                }
                break;
            case 2: engine.forceSpeed = 900; if (alreadySwitched)
                {
                    engine.currentRpm -= 1000;
                    alreadySwitched = false;
                }
                break;
            case 3: engine.forceSpeed = 700; if (!alreadySwitched)
                {
                    engine.currentRpm -= 1000;
                    alreadySwitched = true;
                }
                break;
            case 4: engine.forceSpeed = 500; if (alreadySwitched)
                {
                    engine.currentRpm -= 1000;
                    alreadySwitched = false;
                }
                break;
            case 5: engine.forceSpeed = 400; if (!alreadySwitched)
                {
                    engine.currentRpm -= 1000;
                    alreadySwitched = true;
                }
                break;
            case 6: engine.forceSpeed = 100; if (alreadySwitched)
                {
                    engine.currentRpm -= 1000;
                    alreadySwitched = false;
                }
                break;
        }
    }

    public void ShiftUp()
    {
        if (currentGearNumber < ammountOfGears)
            currentGearNumber++;
        else currentGearNumber = ammountOfGears;
    }
    public void ShiftDown()
    {
        if (currentGearNumber > -1)
            currentGearNumber--;
        else currentGearNumber = -1;
    }

   
}
