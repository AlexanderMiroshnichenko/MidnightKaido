using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float inputThrottle;
    public float inputBrakes;
    public float inputSteering;
    public float inputHandBrake;

    public InputManager _inputs;

    public TrackWaypoints waypoints;
    public Transform currentWaypoint;
    public List<Transform> nodes = new List<Transform>();
    [Range(0, 10)] public int distanceOffset;
    [Range(0, 5)] public float AIstreerForce;
  
    [Range(0, 1)] public float AIThrottle;
  
    internal enum DriveType
    {
        basic,
        AI
    }

    [SerializeField] DriveType driveType;

    [SerializeField] public float steeringSensivity;

   

    private void Awake()
    {
        _inputs = new InputManager();
       // waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<TrackWaypoints>();
        nodes = waypoints.nodes;
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    void Update()
    {
        
        switch (driveType)
        {
            case (DriveType.AI):
                CalculateDistanceOfWayPoints(); AIDrive();
                break;
            case(DriveType.basic): BasicInput();
                break;

        }


       


    }


    private void BasicInput()
    {
        inputThrottle = _inputs.Gameplay.AccBrakes.ReadValue<float>() < 0 ? 0 : _inputs.Gameplay.AccBrakes.ReadValue<float>();
        inputBrakes = _inputs.Gameplay.AccBrakes.ReadValue<float>() > 0 ? 0 : _inputs.Gameplay.AccBrakes.ReadValue<float>();
         inputSteering = Mathf.Lerp(inputSteering, _inputs.Gameplay.Steering.ReadValue<float>(), steeringSensivity * Time.deltaTime);
       // inputSteering = _inputs.Gameplay.Steering.ReadValue<float>();
        inputHandBrake = _inputs.Gameplay.HandBrake.ReadValue<float>();
      
    }


    private void CalculateDistanceOfWayPoints()
    {
        Vector3 position = transform.position;
        float distance = Mathf.Infinity;

        for(int i = 0; i < nodes.Count; i++)
        {
            Vector3 difference = nodes[i].transform.position - position;
            float currentDistance = difference.magnitude;
            if (currentDistance < distance)
            {
                currentWaypoint = nodes[i + distanceOffset];
                distance = currentDistance;
            }
        }
    }

    private void AIDrive()
    {
        
        inputThrottle = AIThrottle;
        AISteer();
       
    }

    private void AISteer()
    {
        Vector3 relative = transform.InverseTransformPoint(currentWaypoint.transform.position);
        relative /= relative.magnitude;

        inputSteering = (relative.x / relative.magnitude) * AIstreerForce;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(currentWaypoint.position, 3);
    }
}
