using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControllerTFM : MonoBehaviour
{

    //common
    private Rigidbody rb;
    private RaycastHit hit;
    private float deltaTime;

    [SerializeField] private LayerMask rayCastLayer;
    //Suspension
    [SerializeField] public float restLength = 0.65f;
    [SerializeField] public float suspensionStiffness = 50000;
    private float lastLength;
    private float currentLength;
    private float springForce;
    private float fZ;
    [SerializeField]
    public float camber;
    //Damper
    [SerializeField] public float damperStiffness = 8000;
    private float damperForce;

    //WheelSettings
    [SerializeField] private GameObject visualMesh;
    [SerializeField] private GameObject visualMeshGLob;
    [SerializeField] public float wheelRadius = 0.34f;
    [SerializeField] public float wheelMass = 15;
    private bool wheelHit;
    private float wheelInertia = 1.5f;

    //Steering 
    [SerializeField] private float steerTime;
    private float steerAngle;
    private float currentAngle;

    //Wheel Velocity
    public Vector3 linearVelocity;
    private float angularVelocity;
    private float angularAcceleration;


    //Target friction method vars
    private float targetAngularVelocity;
    public float targetFrictionTorque;
    public float maximumFrictionTorque;
    [SerializeField]
    private float longFrictionCoefficient = 1f;
    private float targetAngularAcceleration;
    private float frictionTorque;
    public float slipAngle;
    [SerializeField]
    private float slipAnglePeak = 8;
    public float sX;
    public float sY;
    private float fX;
    private float fY;


    //frictionSettings
    [SerializeField] private float relaxationLenth;
    [SerializeField]
    public float latCoeff = 1;
    [SerializeField]
    public float longCoeff = 1;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private ParticleSystem smoke;

    public float slipValueForAudio;

    private float coeff;
    float SADyn;


    //Torque
    private float driveTorque;
    private float brakeTorque;

    private void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        wheelInertia = Mathf.Pow(wheelRadius, 2) * wheelMass;
        audioSource.Play();
        
    }

    public void Steering(float angle)
    {

        // steerAngle = angle;
        steerAngle = Mathf.Lerp(steerAngle, angle, deltaTime * steerTime);
        transform.localRotation = Quaternion.Euler(0, steerAngle, camber);
    }

    public void PhysicsUpdate(float dTorque, float bTorque, float dt)
    {
        driveTorque = dTorque;
        brakeTorque = bTorque;
        deltaTime = dt;
        Raycast();
        ApplyVisuals();
        SimpleDownForce();
        if (!wheelHit) { return; }
        GetSuspensionForce();
        ApplySuspensionForce();
        WheelAcceleration();
        GetSx();
        GetSy();
        AddTireForce();
    }
    private void FixedUpdate()
    {
        ApplyAudio();
        ApplySmoke();
    }
    public void PhysicsUpdate(float dt)
    {
       // brakeTorque = Input.GetAxis("Vertical") > 0 ? 0 : -Input.GetAxis("Vertical") * 2000;
        deltaTime = dt;
        Raycast();
        ApplyVisuals();

        SimpleDownForce();
        if (!wheelHit) { return; }
        GetSuspensionForce();
        ApplySuspensionForce();
        WheelAcceleration();
        GetSx();
        GetSy();
        AddTireForce();
        Debug.Log("??");

    }

    private void Raycast()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, (restLength + wheelRadius), rayCastLayer))
        {
            wheelHit = true;
            currentLength = (transform.position - (hit.point + (transform.up * wheelRadius))).magnitude;
        }
        else
        {
            wheelHit = false;
        }
    }

    private void SimpleDownForce()
    {

    }

    private void GetSuspensionForce()
    {
        springForce = (restLength - currentLength) * suspensionStiffness;
        damperForce = ((lastLength - currentLength) / deltaTime) * damperStiffness;
        fZ = Mathf.Max(0, springForce + damperForce);
        lastLength = currentLength;
    }

    private void ApplySuspensionForce()
    {

       
         rb.AddForceAtPosition(fZ * transform.up, hit.point);
        linearVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
    }

    private void WheelAcceleration()
    {
        frictionTorque = fX * wheelRadius;
        angularAcceleration = (driveTorque - frictionTorque) / wheelInertia;
        angularVelocity += angularAcceleration * deltaTime;
        angularVelocity = Mathf.Clamp(angularVelocity, -360, 360);
        //brakes
        angularVelocity -= Mathf.Min(Mathf.Abs(angularVelocity), brakeTorque * Mathf.Sign(angularVelocity) / wheelInertia * deltaTime);

    }




    private void GetSx()
    {
        targetAngularVelocity = linearVelocity.z / wheelRadius;
        targetAngularAcceleration = (angularVelocity - targetAngularVelocity) / deltaTime;
        targetFrictionTorque = targetAngularAcceleration * wheelInertia;
        maximumFrictionTorque = fZ * wheelRadius * longFrictionCoefficient;
        sX = fZ == 0 ? 0 : targetFrictionTorque / maximumFrictionTorque;
       

    }

    private void GetSy()
    {
       slipAngle = Mathf.Abs(linearVelocity.z) ==0  ? 0 : Mathf.Atan(-linearVelocity.x / Mathf.Abs(linearVelocity.z)) * Mathf.Rad2Deg;
      /*  coeff = (Mathf.Abs(linearVelocity.x) / relaxationLenth) * deltaTime;
        coeff = Mathf.Clamp(coeff, 0f, 1f);
        SADyn += (slipAngle - SADyn) * coeff;
        SADyn = Mathf.Clamp(SADyn, -90f, 90f);
        sY = Mathf.Clamp(SADyn / slipAnglePeak, -1, 1);*/
       sY = slipAngle / slipAnglePeak;
    }

    public float GetSlipRatio()
    {
        return linearVelocity.z == 0 ? 0 : (angularVelocity * wheelRadius) - linearVelocity.z;
    }

    private void AddTireForce()
    {
        Vector3 forwardForceVectorNormalized = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;
        Vector3 sideForceVectorNormalized = Vector3.ProjectOnPlane(transform.right, hit.normal).normalized;
        Vector2 combinedForce = new Vector2(sX, sY);

        if (combinedForce.magnitude > 1)
        {
            combinedForce = combinedForce.normalized;
        }

        //  Debug.Log("Target friction torque " + targetFrictionTorque);
        // Debug.Log("Max friction torque " + maximumFrictionTorque);
        fX = combinedForce.x * fZ * longCoeff;
        fY = combinedForce.y * fZ * latCoeff;




        Vector3 combinedForceNorm = (forwardForceVectorNormalized * fX + sideForceVectorNormalized * fY);
        Debug.DrawRay(hit.point, combinedForceNorm, Color.red);
        rb.AddForceAtPosition(combinedForceNorm,hit.point);
    }

    public void ApplyAntirollBar(float force)
    {
        rb.AddForceAtPosition(force * transform.up, transform.position);
    }

    private void ApplyVisuals()
    {

        currentAngle += angularVelocity * Mathf.Rad2Deg * Time.deltaTime;
        currentAngle %= 360f;

        visualMesh.transform.position = transform.position - transform.up * currentLength;
        visualMesh.transform.localRotation = Quaternion.Euler(currentAngle, steerAngle, 0f);
        visualMeshGLob.transform.localRotation = Quaternion.Euler(0, 0, camber);
    }

    private void ApplyAudio()
    {
        if (Mathf.Abs(sY) > slipValueForAudio || Mathf.Abs(sX) > slipValueForAudio)
            audioSource.volume = Mathf.Clamp(Mathf.Abs(sY + sX), 0, 1);
        else audioSource.volume = 0;
    }

    public float GetWheelAngularVelocity()
    {
        return angularVelocity;
    }
    public float GetWheelInertia()
    {
        return wheelInertia;
    }

    public float GetSuspensionCurrentLength()
    {
        return currentLength;
    }

    public float GetSuspensionRestLength()
    {
        return restLength;
    }
    public bool GetWheelHit()
    {
        return wheelHit;
    }


    public void StartSmoke()
    {
        smoke.Play();
    }
    public void StopSmoke()
    {
        smoke.Stop();
    }

    public void ApplySmoke()
    {
        if (Mathf.Abs(sY) >= 0.5 || Mathf.Abs(sX) >= 0.5)
        {
            StartSmoke();
        }
        else StopSmoke();
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position - new Vector3(0, currentLength, 0), wheelRadius);
    }
}