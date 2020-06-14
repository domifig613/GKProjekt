using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public Action<Collider, PointTrigger> OnTriggerEnterAction = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AICar")
        {
            OnTriggerEnterAction(other, this);
        }
    }
}
