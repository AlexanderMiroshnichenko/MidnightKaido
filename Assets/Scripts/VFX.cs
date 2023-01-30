using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
   [SerializeField] private WheelControllerTFM[] wheel;
    [SerializeField] private TrailRenderer[] trail;
    [SerializeField] private float angleToDrift;
    [SerializeField] private Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (TrailRenderer t in trail)
        {
            foreach (WheelControllerTFM w in wheel)
                if (Mathf.Abs(w.sY) > 0.3&&rb.velocity.magnitude>100)
                {
                    t.emitting = true;

                }
                else if (Mathf.Abs(w.sX)>0.99)
                {
                    t.emitting = true;
                }
                else t.emitting = false;
        }
    }
}
