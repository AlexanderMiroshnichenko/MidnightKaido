using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private RectTransform m_map;
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private float m_offsetX;
    [SerializeField] private float m_offsetY;
    [SerializeField] private float m_scaleX;
    [SerializeField] private float m_scaleY;


    private void FixedUpdate()
    {
        m_map.localPosition = new Vector3(m_playerTransform.position.z * m_scaleY + m_offsetY, m_playerTransform.position.x * m_scaleX + m_offsetX, 0);
        //m_map.localRotation = Quaternion.Euler(new Vector3(0, 0, m_playerTransform.rotation.y));
       // m_map.pivot = new Vector2(Mathf.Clamp01(m_playerTransform.position.x), Mathf.Clamp01(m_playerTransform.position.z));
       
    }

}
