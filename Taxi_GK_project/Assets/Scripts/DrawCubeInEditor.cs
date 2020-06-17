using UnityEngine;

public class DrawCubeInEditor : MonoBehaviour
{
    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
