using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightsController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private bool isTurnedOn=true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)&&!isTurnedOn)
        {
            _animator.SetTrigger("turnOn");
            isTurnedOn = true;


        }
        if (Input.GetKeyDown(KeyCode.O)&&isTurnedOn )
        {
            _animator.SetTrigger("turnOff");
            isTurnedOn = false;

        }
    }
}
