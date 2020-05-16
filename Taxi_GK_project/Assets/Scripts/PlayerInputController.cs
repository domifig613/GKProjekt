using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] List<WheelOnTriggerCollider> wheelsColiders;
    [SerializeField] CanvasGameSceneController canvasGameSceneController;

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

        if(Input.GetKeyDown(KeyCode.M))
        {
            canvasGameSceneController.ChangeMapState();
            ChangeGameStatus();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvasGameSceneController.CloseMap();
            ChangeGameStatus();
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
