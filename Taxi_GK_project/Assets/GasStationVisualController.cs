using System;
using UnityEngine;

public class GasStationVisualController : MonoBehaviour
{
    [SerializeField] private VisualCollider gasStationCollider;
    [SerializeField] private GameObject gasStation;
    [SerializeField] private int yRotationMesh;

    private Transform gasStationVisualTransform;

    private void Start()
    {
        gasStationVisualTransform = gasStation.GetComponent<Transform>();
    }

    private void Update()
    {
        gasStationVisualTransform.Rotate(0, yRotationMesh, 0);
    }

    public bool IsCarInGasStation()
    {
        return gasStationCollider.IsCarInArea;
    }
}
