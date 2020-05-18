using System;
using UnityEngine;

public class VisualCollider : MonoBehaviour
{
    private Action onTriggerStay;
    public bool IsCarInArea {get; private set;}

    public void Init(Action onTriggerStay)
    {
        this.onTriggerStay = onTriggerStay;
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        IsCarInArea = true;
    }

    public void OnTriggerExit(Collider other)
    {
        IsCarInArea = false;    
    }
}
