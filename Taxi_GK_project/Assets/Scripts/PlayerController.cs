using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] List<WheelOnTriggerCollider> wheelsColiders;

    private float verticalInput = 0f;
    private float horizontalInput = 0f;

    private bool wheelsOnStreet = true;

    private void Update()
    {
        wheelsOnStreet = wheelsColiders[0].wheelOnStreet || wheelsColiders[1].wheelOnStreet;

        if (wheelsOnStreet)
        {
            CaptureInput();
            carController.SetInputs(verticalInput, horizontalInput);
        }
        else
        {
            carController.SetInputs(0, 0);
        }
    }

    private void CaptureInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
}
