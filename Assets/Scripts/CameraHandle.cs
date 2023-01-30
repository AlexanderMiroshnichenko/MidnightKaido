using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    [SerializeField]
    private float moveSmoothness;
    [SerializeField]
    private float rotSmoothness;

    [SerializeField]
    private Vector3 moveOffset;
    [SerializeField]
    private Vector3 rotOffset;

    [SerializeField]
    private Transform target;

    private void FixedUpdate()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        HandleMove();

        HandleRotation();
    }
    private void HandleMove()
    {
        Vector3 targetPos = new Vector3();
        targetPos = target.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.fixedDeltaTime);
    }

}
