﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStationController : MonoBehaviour
{
    [SerializeField] List<GasStationVisualController> gasStationVisualControllers;
    [SerializeField] private int priceForFuel;
    [SerializeField] private float fuelPerClick;

    public int PriceForFuel => priceForFuel;
    public float FuelPerClick => fuelPerClick;

    private void Start()
    {
        foreach (var gasStation in gasStationVisualControllers)
        {
            gasStation.gameObject.SetActive(true);
        }
    }
    
    public bool CanGetFuel()
    {
        foreach (var gasStation in gasStationVisualControllers)
        {
            if(gasStation.IsCarInGasStation())
            {
                return true;
            }
        }

        return false;
    }
}
