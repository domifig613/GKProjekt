using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicController : MonoBehaviour
{
    [SerializeField] List<CarPointVisualController> mechanicVisualControllers;
    [SerializeField] private int priceForFixCar;

    public int PriceForFixCar => priceForFixCar;

    private void Start()
    {
        foreach (var mechanic in mechanicVisualControllers)
        {
            mechanic.gameObject.SetActive(true);
        }
    }

    public bool CanFixCar()
    {
        foreach (var mechanic in mechanicVisualControllers)
        {
            if (mechanic.IsCarInCarPoint())
            {
                return true;
            }
        }

        return false;
    }
}