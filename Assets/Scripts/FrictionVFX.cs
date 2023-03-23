using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionVFX : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
     private GameObject particlePrefabClone;
    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.contacts[0];
        particlePrefabClone = Instantiate(particlePrefab, contact.point,new Quaternion(0,0,0,0));
        Debug.Log("Collision");
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(particlePrefabClone);
    }
}
