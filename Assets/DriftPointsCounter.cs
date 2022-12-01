using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftPointsCounter : MonoBehaviour
{
    [SerializeField] Wheel wheel;
    public float angle;
    public float angleToDrift;
    public bool isDrifting;
    public float points;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { Debug.Log(points);
        DriftCheck();
        if (isDrifting)
        {
            points += Time.fixedDeltaTime;
        }
    }
    public void DriftCheck()
    {
        //angle = Vector3.Angle(wheel., rareTrackForce);
        if (Mathf.Abs(angle) >= angleToDrift)
        {
            isDrifting = true;
        }
        else isDrifting = false;
    }
}
