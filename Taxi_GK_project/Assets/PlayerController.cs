using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CarController carController;

    private float verticalInput = 0f;
    private float horizontalInput = 0f;

    private void Update()
    {
        CaptureInput();
        carController.SetInputs(verticalInput, horizontalInput);
    }

    private void CaptureInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
}
