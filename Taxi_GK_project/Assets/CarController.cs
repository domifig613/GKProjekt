using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turningSpeed = 45f;
    [SerializeField] private Rigidbody rigidbody;

    private float currentSpeed = 0f;
    private bool forward = false;

    private void Update()
    {
        currentSpeed = rigidbody.velocity.magnitude;
        forward = (Vector3.Dot(rigidbody.velocity, rigidbody.transform.forward) > 0);
    }

    public void Accelerate()
    {
        if (currentSpeed < maxSpeed)
        {
            rigidbody.AddForce(rigidbody.transform.forward * acceleration);
        }
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
        direction = forward ? direction : direction * -1;

        if (currentSpeed > 0.1 && Mathf.Abs(rigidbody.angularVelocity.y) < 1.5)
        {
            rigidbody.AddTorque(Vector3.up * turningSpeed * direction);
        }
    }
}