using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private RectTransform m_mapPlayerTransform;
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private float m_offsetX;
    [SerializeField] private float m_offsetY;
    [SerializeField] private float m_scaleX;
    [SerializeField] private float m_scaleY;


    private void FixedUpdate()
    {
        m_mapPlayerTransform.localPosition = new Vector3(m_playerTransform.position.x/ m_scaleX + m_offsetX, m_playerTransform.position.z/m_scaleY+m_offsetY, 0);
       // m_mapPlayerTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, m_playerTransform.rotation.y));
       
    }

}
