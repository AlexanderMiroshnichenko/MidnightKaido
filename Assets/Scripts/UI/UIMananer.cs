using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMananer : MonoBehaviour
{
     private GameObject previousElementUI;
    
    public void SetPreviousElemet(GameObject previousElement)
    {
        previousElementUI = previousElement;
    }
    public void OnClickTransition(GameObject nextElement)
    {
        HideElement(previousElementUI);
        ShowElement(nextElement);
    }

    public void HideElement(GameObject element)
    {
        element.SetActive(false);
    }

    public void ShowElement(GameObject element)
    {
        element.SetActive(true);
    }

}
