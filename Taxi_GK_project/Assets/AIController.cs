using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIInputCarController carController;
    [SerializeField] private Route route;
    [SerializeField] private int startPositionIndex = 0;


    private void Start()
    {
        route.OnRouteTriggerAction += OnPointChanged;

        SetStartCarParams();

        carController.SetNewPoint(route.GetNextPoint(startPositionIndex));
    }

    private void SetStartCarParams()
    {
        Vector3 carStartPosition = route.GetPoint(startPositionIndex);
        Vector3 nextCarPosition = route.GetNextPoint(startPositionIndex);
        carController.transform.position = carStartPosition;
        carController.transform.LookAt(nextCarPosition);
        carController.gameObject.SetActive(true);
    }

    private void OnPointChanged(Collider collider, int index)
    {
        if(collider == carController.CarCollider)
        {
            carController.SetNewPoint(route.GetNextPoint(index));
        }
    }
}
