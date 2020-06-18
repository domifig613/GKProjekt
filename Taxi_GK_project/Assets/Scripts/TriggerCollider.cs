using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public bool collisionDetected { private set; get; }
    public bool collisionWithOtherDetected { private set; get; }
    
    private int collisionWithWallCount = 0;
    private int collisionWithOtherCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collisionWithWallCount++;
        }
        else if (other.transform.tag == "AICar")
        {
            collisionWithOtherCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collisionWithWallCount--;
        }
        else if (other.transform.tag == "AICar")
        {
            collisionWithOtherCount--;
        }
    }

    private void Update()
    {
        collisionDetected = collisionWithWallCount > 0;
        collisionWithOtherDetected = collisionWithOtherCount > 0;
    }
}
