using System;
using UnityEngine;

public class QuestVisualCollider : MonoBehaviour
{
    private Action onTriggerStay;

    public void Init(Action onTriggerStay)
    {
        this.onTriggerStay = onTriggerStay;
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke();
    }
}
