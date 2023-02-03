using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
  
    [Range(1, 10000)]
    [SerializeField] float frequency;
    [SerializeField] Debugger debug;
    [SerializeField] WheelControllerTFM[] wheelControllers;
    [SerializeField] EngineComponent engine;
    [SerializeField] SteeringComponent steering;
    [SerializeField] GearBoxComponent gearBox;
    [SerializeField] ClutchComponent clutchComponent;
    [SerializeField] DifferentialComponent differential;
    [SerializeField] BrakesComponent brakes;
    
    [SerializeField] AntiRollBarComponent antirollBar;
    [SerializeField] Dashboard dashboard;

  
    [SerializeField] private InputController _inputController;


    [SerializeField] KeyCode shiftUpBtn;
    [SerializeField] KeyCode shiftDownBtn;
    [SerializeField] KeyCode clutchBtn;
    [SerializeField] KeyCode handBrakeBtn;
   
    
    private Rigidbody rb;
    private float downForce;
    private float deltaTime;
    public float inputThrottle;
    private float inputBrakes;
    private float inputSteering;
    private float angleL;
    private float angleR;
    private float clutch;
    private float btR; // brake torque rear
    private bool handBrake;
    private float[] wheelTorque = new float[2];
    private float[] angularVelocities = new float[4];
    private float inputHandBrake;

    

    
    private void Awake()
    {
       
        
        _inputController._inputs.Gameplay.ShiftDown.performed += contex => gearBox.ChangeGearDown();
        _inputController._inputs.Gameplay.ShiftUp.performed += contex => gearBox.ChangeGearUp(); 




        rb = transform.root.GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
       
    }
    private void Start()
    {
        engine.InitializeEngine(rb, gearBox);
        steering.InitializeSteering(wheelControllers);
        clutchComponent.InitializeClutch();

        dashboard.InitDashboard(rb, 10000f);
        antirollBar.InitializeAntirollBar(wheelControllers);
        differential.InitializeDifferential(wheelControllers);
    }
  
    private void HandBrake()
    {
        handBrake = true;
        Debug.Log("HAND");
    }

    private void Update()
    {
       
       
            GearBoxShifterSim();
            dashboard.UpdateD(engine.GetRpm());
        
        
    }



    private void FixedUpdate()
    {
        
        
            InputUpdate();
            UpdateSteering();
            UpdateDownForce();
            deltaTime = Time.fixedDeltaTime;
            UpdatePhysics();
            
        
       
        
    }

    private void InputUpdate()
    {


        /*inputThrottle = Input.GetAxis("Vertical") < 0 ? 0 : Input.GetAxis("Vertical");
        inputBrakes = Input.GetAxis("Vertical") > 0 ? 0 : Input.GetAxis("Vertical");
        inputSteering = Input.GetAxis("Horizontal");*/

        inputThrottle = _inputController.inputThrottle;
        inputBrakes = _inputController.inputBrakes;
        inputSteering = _inputController.inputSteering;
        inputHandBrake = _inputController.inputHandBrake;

        if (Input.GetKey(clutchBtn))
        {
            clutch = Mathf.Lerp(clutch, 1, Time.deltaTime);
        }
        else
        {
            clutch = Mathf.Lerp(clutch, 0, Time.deltaTime); ;
        }
        /*  if (Input.GetKey(handBrakeBtn))
          {

          }
          else 
          {
              handBrake = false;
          }*/
        handBrake = false;
    }

    private void UpdatePhysics()
    {
        debug.Line5(rb.velocity.magnitude * 3.6f);
        antirollBar.CalculateAntirollBar();
        
            SimDriveTrain();
            debug.Line1(engine.GetRpm());
            debug.Line2(clutchComponent.GetClutchTorque());
            debug.Line3(clutchComponent.GetLock());
            debug.Line4(gearBox.GetCurrentGear());
        
    }

 

    private void SimDriveTrain()
    {
        var gbxTorque = gearBox.GetOutputTorque(clutchComponent.GetClutchTorque());
        wheelTorque = differential.GetOutputTorque(gbxTorque);
        UpdateWheels(wheelTorque, brakes.GetBrakes(inputBrakes, angularVelocities));
        var whAVL = wheelControllers[2].GetWheelAngularVelocity();
        var whAVR = wheelControllers[3].GetWheelAngularVelocity();
        var dInputShaftVel = differential.GetInputShaftVelocity(whAVL, whAVR);
        var gBoxInShaftVel = gearBox.GetInputShaftVelocity(dInputShaftVel);
        clutchComponent.UpdatePhysics(gBoxInShaftVel, engine.GetAngularVelocity(), gearBox.GetGearBoxRatio());
        engine.UpdatePhysics(deltaTime, inputThrottle, clutchComponent.GetClutchTorque());
    }

    private void UpdateSteering()
    {
        steering.PhysicsUpdate(inputSteering);
        angleL = steering.GetSteerAngles()[0];
        
        angleR = steering.GetSteerAngles()[1];
        wheelControllers[0].Steering(angleL);
        wheelControllers[1].Steering(angleR);
    }

    private void UpdateDownForce()
    {
        //Credits BlinkAChu
        Vector3 linearVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(transform.position));
        downForce = 0.5f * 1.22f * Mathf.Pow((Mathf.Max(0, linearVelocity.z)), 2) * (5f * 2f);
        rb.AddForceAtPosition(-transform.up * downForce, transform.position);
    }

    private void UpdateWheels(float[] _driveTorque, float[] _brakeTorque)
    {
        if(inputHandBrake>0)
        {
            
            btR = Mathf.Lerp(btR, 5000f, Time.deltaTime * 8f);
        }
        else
        {

            btR = _brakeTorque[1];
        }
        
     

        wheelControllers[0].PhysicsUpdate(0, _brakeTorque[0], deltaTime);
        wheelControllers[1].PhysicsUpdate(0, _brakeTorque[0], deltaTime);
        wheelControllers[2].PhysicsUpdate(_driveTorque[0], btR, deltaTime);
        wheelControllers[3].PhysicsUpdate(_driveTorque[1], btR, deltaTime);
    }

   

    private void GearBoxShifterSim()
    {
        //ShiftUp
       /* if (Input.GetKeyDown(shiftUpBtn))
        {
            gearBox.ChangeGearUp();
        }
        //ShiftDown
        if (Input.GetKeyDown(shiftDownBtn))
        {
            gearBox.ChangeGearDown();
        }*/
    }

    public WheelControllerTFM[] GetWheels()
    {
        return wheelControllers;
    }
}
