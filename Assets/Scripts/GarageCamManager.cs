using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCamManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;

    public void CameraChange()
    {
        if (cameras[0].active)
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            cameras[2].SetActive(false);
        }
        if (cameras[1].active)
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(false);
            cameras[2].SetActive(true);
        }
        if (cameras[2].active)
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
            cameras[2].SetActive(false);
        }



    }
}
