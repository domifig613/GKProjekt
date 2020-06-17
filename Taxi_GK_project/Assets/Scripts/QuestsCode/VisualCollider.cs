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
        if (other.gameObject.tag == "Player")
        {
            onTriggerStay?.Invoke();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsCarInArea = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsCarInArea = false;
        }
    }
}
