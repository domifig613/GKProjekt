using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{ 
    [SerializeField] private List<PointTrigger> points;

    public Action<Collider, int> OnRouteTriggerAction = delegate { };

    public int PointsLenght => points.Count;

    private void Start()
    {
        foreach (var point in points)
        {
            point.OnTriggerEnterAction += OnPointOnRouteTrigger;
        }
    }

    public void OnPointOnRouteTrigger(Collider collider, PointTrigger point)
    {
        int index = points.IndexOf(point);
        OnRouteTriggerAction(collider, index);
    }

    public Vector3 GetNextPoint(int currentPointIndex)
    {
        if(points.Count > currentPointIndex + 1)
        {
            return points[currentPointIndex + 1].transform.position;
        }
        else
        {
            return points[0].transform.position;
        }
    }

    public Vector3 GetPoint(int pointIndex)
    {
        return points[pointIndex].transform.position;
    }
}
