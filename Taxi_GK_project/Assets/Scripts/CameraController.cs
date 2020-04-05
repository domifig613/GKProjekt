using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float cameraSpeed;

    private Camera camera;
    private Vector3 distanceFromTarget;

    private void Start()
    {
        camera = GetComponent<Camera>();
        distanceFromTarget = camera.transform.position - targetTransform.position;
    }

    private void FixedUpdate()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetTransform.position + distanceFromTarget, Time.deltaTime * cameraSpeed);
    }
}
