using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum GearBoxType
    {
        manual,
        auto
    }

    [SerializeField] public GearBoxType gearBoxType;
    private CustomFixedUpdate m_FixedUpdate;
    [SerializeField] private bool useCustomFixedUpdate;
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
    [SerializeField] private StopLightsController m_stopLightsController;

  
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
        rb = transform.root.GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;

        if (useCustomFixedUpdate)
        {
            frequency = Mathf.Ceil(Mathf.Clamp(frequency, 1, Mathf.Infinity));
            deltaTime = 1 / frequency;
            m_FixedUpdate = new CustomFixedUpdate(deltaTime, UpdateAtCustomTimestep);
        }

    }
    private void Start()
    {


        if (gearBoxType == GearBoxType.manual)
        {
            _inputController._inputs.Gameplay.ShiftDown.performed += contex => gearBox.ChangeGearDown();
            _inputController._inputs.Gameplay.ShiftUp.performed += contex => gearBox.ChangeGearUp();
        }

        engine.InitializeEngine(rb, gearBox);
        steering.InitializeSteering(wheelControllers);
        clutchComponent.InitializeClutch();
        dashboard.InitDashboard(rb, 10000f);
        antirollBar.InitializeAntirollBar(wheelControllers);
        differential.InitializeDifferential(wheelControllers);


    }
    private void UpdateAtCustomTimestep()
    {
        //CUSTOM UPDATE RATE IS PROVIDED BY AESTHETIC
        if (useCustomFixedUpdate)
        {
            UpdatePhysics();
            Debug.Log("!!!");
        }
    }


    private void Update()
    {
        if (useCustomFixedUpdate)
        {
            m_FixedUpdate.Update(Time.deltaTime);
        }

        if (gearBoxType == GearBoxType.auto)
        {
            AutomaticShifter();
        }
            dashboard.UpdateD(engine.GetRpm());
        inputThrottle = _inputController.inputThrottle;
        inputBrakes = _inputController.inputBrakes;
        inputSteering = _inputController.inputSteering;
        inputHandBrake = _inputController.inputHandBrake;

        if (Mathf.Abs(inputBrakes) > 0&& m_stopLightsController!=null)
        {
            m_stopLightsController.TurnLightsOn();
        }
        else { 
            if(m_stopLightsController!=null)
            m_stopLightsController.TurnLightsOff(); 
        }


    }



    private void FixedUpdate()
    {
        
        
     
        if (!useCustomFixedUpdate)
        {
            InputUpdate();
            UpdateSteering();
            UpdateDownForce();
            deltaTime = Time.fixedDeltaTime;
            UpdatePhysics();
          
        }
        else
        {
            
            InputUpdate();
            UpdateSteering();
            UpdateDownForce();
        }


    }

    private void InputUpdate()
    {
   

        if (Input.GetKey(clutchBtn))
        {
            clutch = Mathf.Lerp(clutch, 1, Time.deltaTime);
        }
        else
        {
            clutch = Mathf.Lerp(clutch, 0, Time.deltaTime); ;
        }
       
        
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



    private void AutomaticShifter()
    {
      
        if (engine.engineRpm >= 9000 && gearBox.inGear)
        {
            gearBox.ChangeGearUp();
        }
        if (engine.engineRpm <= 5000 && gearBox.inGear && gearBox.currentGear > 2)
        {
            gearBox.ChangeGearDown();
        }
    }

    public WheelControllerTFM[] GetWheels()
    {
        return wheelControllers;
    }
}
