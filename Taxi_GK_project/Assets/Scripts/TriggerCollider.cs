﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public bool collisionDetected { private set; get; }
    
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
        collisionDetected = collisionCount > 0;
    }
}