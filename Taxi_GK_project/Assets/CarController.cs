using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turningSpeed = 45f;
    [SerializeField] private AnimationCurve turningFactorBySpeed;

    private float currentSpeed = 0f;

    private float verticalInput = 0;
    private float horizontalInput = 0;

    private void Update()
    {
        CaptureInput();
    }

    private void FixedUpdate()
    {
        HandleTurning();
        HandleMoving();
    }

    private void CaptureInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleTurning()
    {
        if (Mathf.Abs(currentSpeed) > 0.1)
        {
            float inputFactor = currentSpeed >= 0 ? horizontalInput : -horizontalInput;
            float turnAngle = inputFactor * turningSpeed *  Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, turnAngle, Space.World);
        }
    }

    private void HandleMoving()
    {
        float verticalInputFactor = verticalInput;

        if (currentSpeed > 0.1 && verticalInput <= 0)
        {
            verticalInputFactor = verticalInput - 1;
        }

        if (currentSpeed < -0.1 && verticalInput <= 0)
        {
            verticalInputFactor = verticalInput + 0.3f;
        }

        currentSpeed += verticalInputFactor * acceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2f, maxSpeed);

        //Debug.Log(currentSpeed);

        if (verticalInput == 0 && Mathf.Abs(currentSpeed) < 0.1)
        {
            currentSpeed = 0;
        }

        transform.position += transform.forward * currentSpeed * Time.fixedDeltaTime;
    }
}