using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private bool isEnabled;
    [SerializeField] private Transform needleTacho;
    [SerializeField] private Transform needleSpeed;
    [SerializeField] private Text speedText;
    [SerializeField] private TMP_Text gear;
    [SerializeField] private GearBoxComponent gearComponent;
    private float tachoAngle;
    private float speedAngle;
    [SerializeField] private float zeroTachoAngle = 36f;
    [SerializeField] private float maxTachoAngle = -130f;
    [SerializeField] private float zeroSpeedAngle = 36f;
    [SerializeField] private float maxSpeedAngle = -130f;
    private float tachoAngleSize;
    private float speedAngleSize;
    private float engineMaxRpm;
    private float speed;
    private Rigidbody rb;
    //[SerializeField] private Text angle;
    [SerializeField] private WheelControllerTFM wheel;
    public void InitDashboard(Rigidbody _rb, float maxRpm)
    {
        engineMaxRpm = maxRpm;
        rb = _rb;
        tachoAngleSize = zeroTachoAngle - maxTachoAngle;
        speedAngleSize = zeroSpeedAngle - maxSpeedAngle;
    }

    // Update is called once per frame
    public void UpdateD(float engineRpm)
    {
        if (isEnabled)
        {
            float rpmNormalized = engineRpm / engineMaxRpm;
            tachoAngle = zeroTachoAngle - rpmNormalized * tachoAngleSize;
            needleTacho.eulerAngles = new Vector3(0, 0, tachoAngle);


            float speedNormalized = speed / 280f;

            speedAngle = zeroSpeedAngle - speedNormalized * speedAngleSize;
            needleSpeed.eulerAngles = new Vector3(0, 0, speedAngle);

            speed = Mathf.Round(rb.velocity.magnitude * 3.6f);


            // speedText.text = Mathf.Round(rb.velocity.magnitude * 3.6f).ToString();
            //angle.text = "Angle: "+Mathf.RoundToInt(Mathf.Abs(wheel.slipAngle)).ToString();
            if (gearComponent.GetCurrentGear()==1)
            gear.text = "N";
            else gear.text =(gearComponent.GetCurrentGear() - 1).ToString();
            if (gearComponent.GetCurrentGear() == 0)
                gear.text = "R";
           
            

        }
    }
}
