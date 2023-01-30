using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMananer : MonoBehaviour
{
    [SerializeField] private GameObject carSettings;
    
    public void OnClickClose(GameObject button)
    {
        carSettings.SetActive(false);
        button.SetActive(true);
    }
    public void OnClickOpen(GameObject button)
    {
        carSettings.SetActive(true);
        button.SetActive(false);
    }
}
