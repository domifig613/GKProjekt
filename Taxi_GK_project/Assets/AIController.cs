using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIInputCarController carController;
    [SerializeField] private Route route;
    [SerializeField] private int startPositionIndex = 0;

    private Stopwatch stopwatch = new Stopwatch();
    private Transform children;

    public void Init(Route route, AIInputCarController carController, int startPositionIndex)
    {
        this.route = route;
        this.carController = carController;
        this.startPositionIndex = startPositionIndex;
        this.children = carController.transform.GetComponentInChildren<Rigidbody>().transform;
    }

    private void Start()
    {
        route.OnRouteTriggerAction += OnPointChanged;
        InitCar();
    }

    private void Update()
    {
        if(stopwatch.Elapsed.Seconds > 20)
        {
            UnityEngine.Debug.Log("reset AI");
            stopwatch.Restart();
            stopwatch.Start();
            InitCar();
        }
    }

    private void InitCar()
    {
        SetStartCarParams();
        carController.SetNewPoint(route.GetNextPoint(startPositionIndex));
    }

    private void SetStartCarParams()
    {
        Vector3 carStartPosition = route.GetPoint(startPositionIndex);
        Vector3 nextCarPosition = route.GetNextPoint(startPositionIndex);
        carController.transform.position = new Vector3(0, 0, 0);
        children.SetPositionAndRotation(carStartPosition, new Quaternion());
        children.LookAt(nextCarPosition);
        carController.gameObject.SetActive(true);
        stopwatch.Start();
    }

    private void OnPointChanged(Collider collider, int index)
    {
        if(collider == carController.CarCollider)
        {
            stopwatch.Restart();
            stopwatch.Start();

            carController.SetNewPoint(route.GetNextPoint(index));
        }
    }
}
