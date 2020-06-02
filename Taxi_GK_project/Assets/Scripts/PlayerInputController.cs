using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] TriggerCollider frontBumper;
    [SerializeField] TriggerCollider rearBumper;
    [SerializeField] CanvasGameSceneController canvasGameSceneController;
    [SerializeField] GasStationController gasStationController;
    [SerializeField] MechanicController mechanicController;

    private float verticalInput = 0f;
    private float horizontalInput = 0f;
    private bool wasCollisionWithWallInLastCheck = false;

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
            if(mechanicController.CanFixCar() && PlayerController.Cash >= mechanicController.PriceForFixCar)
            {
                if(carController.TryAddDurability())
                {
                    PlayerController.RemoveCash(mechanicController.PriceForFixCar);
                }
            }
        }
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
}
