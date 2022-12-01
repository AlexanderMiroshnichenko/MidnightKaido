using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private Vector3 target;
    public Vector3 offset;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

   
    void FixedUpdate()
    {
        Follow();
    }
    private void Follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, target, Time.fixedDeltaTime * speed);
        
        gameObject.transform.LookAt(Player.gameObject.transform.position);
        target = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z) + offset;

    }
}
