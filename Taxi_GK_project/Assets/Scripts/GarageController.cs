using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageController : MonoBehaviour
{
    [SerializeField] private List<CarPointVisualController> garageVisualControllers;
    [SerializeField] private List<GameObject> cars;

    List<int> indexOfBoughtCars;

    private void Start()
    {
        indexOfBoughtCars = new List<int>();
        indexOfBoughtCars.Add(0);

        foreach (var garage in garageVisualControllers)
        {
            garage.gameObject.SetActive(true);
        }
    }

    public bool CanOpenGarage()
    {
        foreach (var garage in garageVisualControllers)
        {
            if (garage.IsCarInCarPoint())
            {
                return true;
            }
        }

        return false;
    }

    public void BuyCar(int index)
    {
        indexOfBoughtCars.Add(index);
    }

    public bool CheckCarBuyingStatus(int index)
    {
        return indexOfBoughtCars.Contains(index);
    }

    public GameObject GetCar(int index)
    {
        return Instantiate(cars[index]);
    }

    public Vector3 GetFirstGaragePosition()
    {
        return garageVisualControllers[0].transform.position;
    }
}
