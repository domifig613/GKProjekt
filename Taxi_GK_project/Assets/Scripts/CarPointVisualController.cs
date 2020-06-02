using System;
using UnityEngine;

public class CarPointVisualController : MonoBehaviour
{
    [SerializeField] private VisualCollider carPointCollider;
    [SerializeField] private GameObject carPointGameObject;
    [SerializeField] private int yRotationMesh;

    private Transform carPointVisualTransform;

    private void Start()
    {
        carPointVisualTransform = carPointGameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        carPointVisualTransform.Rotate(0, yRotationMesh, 0);
    }

    public bool IsCarInCarPoint()
    {
        return carPointCollider.IsCarInArea;
    }
}
