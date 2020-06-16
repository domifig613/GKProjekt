using UnityEngine;

public class DrawSphereInEditor : MonoBehaviour
{
    [SerializeField] Color color;

    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
