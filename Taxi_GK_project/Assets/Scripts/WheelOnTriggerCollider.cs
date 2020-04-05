using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOnTriggerCollider : MonoBehaviour
{
    public bool wheelOnStreet { private set; get; }
    
    private int collisionCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        collisionCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        collisionCount--;
    }

    private void Update()
    {
        wheelOnStreet = collisionCount > 0;
    }
}
