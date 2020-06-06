using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] TriggerCollider frontBumper;
    [SerializeField] TriggerCollider rearBumper;
    [SerializeField] CanvasGameSceneController canvasGameSceneController;
    [SerializeField] GasStationController gasStationController;
    [SerializeField] MechanicController mechanicController;
    [SerializeField] GarageController garageController;

    private float verticalInput = 0f;
    private float horizontalInput = 0f;
    private bool wasCollisionWithWallInLastCheck = false;
    private bool mechanicCorStart = false;

    private Vector3 startPosition = new Vector3(65.3f, 0f, -245.5f);
    private Vector3 mechanicPosition = new Vector3(65.3f, 0f, -230.5f);

    private void Start()
    {
        carController.SetPosition(startPosition);
    }

    private void Update()
    {
        CaptureInput();

        if (!wasCollisionWithWallInLastCheck && (frontBumper.collisionDetected || rearBumper.collisionDetected))
        {
            wasCollisionWithWallInLastCheck = !wasCollisionWithWallInLastCheck;
            carController.RemoveDurability(5);
        }
        else
        {
            wasCollisionWithWallInLastCheck = frontBumper.collisionDetected || rearBumper.collisionDetected;
        }

        carController.SetInputs(verticalInput, horizontalInput, frontBumper.collisionDetected, rearBumper.collisionDetected);

        if (Input.GetKeyDown(KeyCode.M))
        {
            canvasGameSceneController.ChangeMapState();
            ChangeGameStatus();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasGameSceneController.CloseMap();
            ChangeGameStatus();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (gasStationController.CanGetFuel())
            {
                if (PlayerController.Cash > 0)
                {
                    if (carController.TryAddFuel(gasStationController.FuelPerClick))
                    {
                        PlayerController.RemoveCash(gasStationController.PriceForFuel);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (mechanicController.CanFixCar() && PlayerController.Cash >= mechanicController.PriceForFixCar)
            {
                if (carController.TryAddDurability())
                {
                    PlayerController.RemoveCash(mechanicController.PriceForFixCar);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (garageController.CanOpenGarage())
            {
                canvasGameSceneController.OpenGarage();
            }
        }
        else if (Input.GetKey(KeyCode.H) && !mechanicCorStart && carController.CurrentSpeed <= 1.0f)
        {
            StartCoroutine(CarToMechanicPositionCor());
        }
    }

    private IEnumerator CarToMechanicPositionCor()
    {
        mechanicCorStart = true;
        yield return new WaitForSeconds(0.5f);

        if (Input.GetKey(KeyCode.H) && carController.CurrentSpeed <= 1.0f)
        {
            yield return new WaitForSeconds(0.5f);

            if (Input.GetKey(KeyCode.H) && carController.CurrentSpeed <= 1.0f)
            {
                yield return new WaitForSeconds(0.5f);

                if (Input.GetKey(KeyCode.H) && carController.CurrentSpeed <= 1.0f)
                {
                    carController.SetPosition(mechanicPosition);
                }
            }
        }

        mechanicCorStart = false;
    }

    private void ChangeGameStatus()
    {
        if (canvasGameSceneController.MapIsOpen())
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void CaptureInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    public void SetNewCar(CarController carController, TriggerCollider frontBumper, TriggerCollider rearBumper)
    {
        this.carController = carController;
        this.frontBumper = frontBumper;
        this.rearBumper = rearBumper;
    }
}
