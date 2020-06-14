using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AIInputCarController : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [SerializeField] private Collider carCollider;
    [SerializeField] private GameObject model;

    private Vector3 nextPoint;
    public Collider CarCollider => carCollider;

    public void SetNewPoint(Vector3 newPoint)
    {
        nextPoint = newPoint;
    }

    private void Update()
    {
        Vector3 heading = (nextPoint - model.transform.position);
        float distance = heading.magnitude;
        Vector3 directionVector = heading/distance;

        float directionFloat = Vector3.Dot(directionVector, model.transform.right);
        float directionWithRound = Mathf.Round(directionFloat * 100f) / 100f;

        carController.SetInputs(1, directionWithRound* 2, false, false);
    }
}
