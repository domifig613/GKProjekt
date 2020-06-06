using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turningSpeed = 45f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float maxFuel;
    [SerializeField] private float fuelConsumption;
    [SerializeField] private float maxDurability;
    [SerializeField] private float currentDurability;

    public float CurrentSpeed { get;  private set; } = 0f;
    private float verticalValue = 0;
    private float horizontalValue = 0;
    private float currentFuel;
    private bool frontCollision = false;
    private bool rearCollision = false;

    private void Start()
    {
        currentFuel = maxFuel;
        currentDurability = maxDurability;
    }

    public void SetPosition(Vector3 newPos)
    {
        rigidbody.gameObject.transform.localPosition = newPos;
    }

    public void RemoveDurability(int durabilityToRemove)
    {
        currentDurability -= durabilityToRemove;

        if(currentDurability < 0)
        {
            currentDurability = 0;
        }
    }

    public float GetCurrentFuelPart()
    {
        return currentFuel / maxFuel;
    }

    public float GetCurrentDurability()
    {
        return currentDurability / maxDurability;
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

    public bool TryAddDurability()
    {
        if (currentDurability < maxDurability)
        {
            currentDurability = maxDurability;
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
        if (Mathf.Abs(CurrentSpeed) > 0.1)
        {
            float inputFactor = CurrentSpeed >= 0 ? horizontalValue : -horizontalValue;
            float turnAngle = inputFactor * turningSpeed * Mathf.Abs(CurrentSpeed) / 10 * Time.fixedDeltaTime;
            rigidbody.transform.Rotate(Vector3.up, turnAngle, Space.World);
        }
    }

    private void HandleMoving()
    {
        if(currentFuel <= 0f || currentDurability <=0f)
        {
            verticalValue = 0f;
        }
        else
        {
            currentFuel -= Mathf.Abs(verticalValue * CurrentSpeed * fuelConsumption);
        }

        float verticalInputFactor = verticalValue;

        if (CurrentSpeed > 0.1 && verticalValue <= 0f)
        {
            verticalInputFactor = verticalValue - 0.3f;
        }
        else if (CurrentSpeed < -0.1 && verticalValue <= 0f)
        {
            verticalInputFactor = verticalValue + 0.3f;
        }


        CurrentSpeed += verticalInputFactor * acceleration * Time.fixedDeltaTime;
        CurrentSpeed = Mathf.Clamp(CurrentSpeed, -maxSpeed / 2f, maxSpeed);

        if (verticalValue == 0 && Mathf.Abs(CurrentSpeed) < 0.1)
        {
            CurrentSpeed = 0;
        }

        Vector3 forward = new Vector3(rigidbody.transform.forward.x, 0f, rigidbody.transform.forward.z);
            
        if(rearCollision && CurrentSpeed < 0f)
        {
            CurrentSpeed = 0f;
        }
        else if(frontCollision && CurrentSpeed > 0f)
        {
            CurrentSpeed = 0f;
        }

        rigidbody.transform.position += forward * CurrentSpeed * Time.fixedDeltaTime;
    }
}