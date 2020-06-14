using UnityEngine;

public class DrawSphereInEditor : MonoBehaviour
{
    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(0, 1, 1, 0.8f);
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
