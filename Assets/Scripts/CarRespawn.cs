using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRespawn : MonoBehaviour
{

    public Transform carOnTouch;
    public Vector3 positionOnTouch;

    public Quaternion rotationOnTouch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CheckPoint>())
        {
            positionOnTouch=transform.position;
            rotationOnTouch=transform.rotation;


        }

    }

    public void OnRespawn()
    {
        
           transform.SetPositionAndRotation(positionOnTouch,rotationOnTouch);
        


    }

}
