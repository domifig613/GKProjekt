using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turningSpeed = 45f;
    [SerializeField] private AnimationCurve turningFactorBySpeed;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float maxFuel;
    [SerializeField] private float fuelConsumption;

    private float currentSpeed = 0f;
    private float verticalValue = 0;
    private float horizontalValue = 0;
    private float currentFuel;
    private bool frontCollision = false;
    private bool rearCollision = false;

    private void Start()
    {
        currentFuel = maxFuel;
    }

    public float GetCurrentFuelPart()
    {
        return currentFuel / maxFuel;
    }

    public bool TryAddFuel(float fuel)
    {
        if (currentFuel < maxFuel)
        {
            currentFuel += fuel;

            if(currentFuel > maxFuel)
            {
                currentFuel = maxFuel;
            }

            return true;
        }

        return false;
    }

    public void SetInputs(float verticalValue, float horizontalValue, bool frontCollision, bool rearCollision)
    {
        this.verticalValue = verticalValue;
        this.horizontalValue = horizontalValue;
        this.frontCollision = frontCollision;
        this.rearCollision = rearCollision;
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
            float turnAngle = inputFactor * turningSpeed * Mathf.Abs(currentSpeed) / 10 * Time.fixedDeltaTime;
            rigidbody.transform.Rotate(Vector3.up, turnAngle, Space.World);
        }
    }

    private void HandleMoving()
    {
        if(currentFuel <= 0f)
        {
            verticalValue = 0f;
        }
        else
        {
            currentFuel -= Mathf.Abs(verticalValue * currentSpeed * fuelConsumption);
        }

        float verticalInputFactor = verticalValue;

        if (currentSpeed > 0.1 && verticalValue <= 0f)
        {
            verticalInputFactor = verticalValue - 0.3f;
        }
        else if (currentSpeed < -0.1 && verticalValue <= 0f)
        {
            verticalInputFactor = verticalValue + 0.3f;
        }


        currentSpeed += verticalInputFactor * acceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2f, maxSpeed);

        if (verticalValue == 0 && Mathf.Abs(currentSpeed) < 0.1)
        {
            currentSpeed = 0;
        }

        Vector3 forward = new Vector3(rigidbody.transform.forward.x, 0f, rigidbody.transform.forward.z);
            
        if(rearCollision && currentSpeed < 0f)
        {
            currentSpeed = 0f;
        }
        else if(frontCollision && currentSpeed > 0f)
        {
            currentSpeed = 0f;
        }

        rigidbody.transform.position += forward * currentSpeed * Time.fixedDeltaTime;
    }
}