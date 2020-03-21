using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turningSpeed = 45f;
    [SerializeField] private AnimationCurve turningFactorBySpeed;
    [SerializeField] private Rigidbody rigidbody;

    private float currentSpeed = 0f;

    private float verticalInput = 0;
    private float horizontalInput = 0;

   
    public void Accelerate()
    {
        //   Debug.Log(rigidbody.velocity.magnitude);

        if (currentSpeed < maxSpeed)
        {
            rigidbody.AddForce(rigidbody.transform.forward * acceleration);
        }
    }

    private void Update()
    {
        currentSpeed = rigidbody.velocity.magnitude;
    }

    public void Break()
    {
        if (currentSpeed < maxSpeed)
        {
            rigidbody.AddForce(-rigidbody.transform.forward * acceleration);
        }
    }

    public void ChangeDirection(bool left)
    {
        float direction = left ? -1 : 1;
        
     //   if (currentSpeed > 0.1 && rigidbody.angularVelocity.magnitude < 3)
        {
            Debug.Log(Vector3.up * turningSpeed * direction);
            rigidbody.AddTorque(Vector3.up * turningSpeed * direction, ForceMode.Force);
        }

      //  Debug.Log(rigidbody.angularVelocity.magnitude);
    }
}