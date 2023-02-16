using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWaypoints : MonoBehaviour
{
    public Color lineColor;
    public List<Transform> nodes = new List<Transform>();
    [Range(0, 1)] public float sphereRadius;
   
    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        Transform[] path = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 1; i < path.Length; i++)
        {
            nodes.Add(path[i]);
        }

        for(int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentWaypoint = nodes[i].position;
            Vector3 previousWaypoint = Vector3.zero;

            if (i != 0) previousWaypoint = nodes[i - 1].position;

            else if (i == 0) previousWaypoint = currentWaypoint;

            Gizmos.DrawLine(previousWaypoint, currentWaypoint);
            Gizmos.DrawSphere(currentWaypoint, sphereRadius);
        }

    }
}
