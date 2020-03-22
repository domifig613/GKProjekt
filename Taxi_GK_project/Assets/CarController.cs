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
    private float verticalValue = 0;
    private float horizontalValue = 0;

    public void SetInputs(float verticalValue, float horizontalValue)
    {
        this.verticalValue = verticalValue;
        this.horizontalValue = horizontalValue;
    }

    private void FixedUpdate()
    {
        HandleTurning();
        HandleMoving();
    }

    private void HandleTurning()
    {
        if (Mathf.Abs(currentSpeed) > 0.1)
        {
            float inputFactor = currentSpeed >= 0 ? horizontalValue : -horizontalValue;
            float turnAngle = inputFactor * turningSpeed * Mathf.Abs(currentSpeed)/10 *  Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, turnAngle, Space.World);
        }
    }

    private void HandleMoving()
    {
        float verticalInputFactor = verticalValue;

        if (currentSpeed > 0.1 && verticalValue <= 0)
        {
            verticalInputFactor = verticalValue - 0.3f;
        }
        else if (currentSpeed < -0.1 && verticalValue <= 0)
        {
            verticalInputFactor = verticalValue + 0.3f;
        }

        currentSpeed += verticalInputFactor * acceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2f, maxSpeed);

        if (verticalValue == 0 && Mathf.Abs(currentSpeed) < 0.1)
        {
            currentSpeed = 0;
        }

        transform.position += transform.forward * currentSpeed * Time.fixedDeltaTime;
    }
}