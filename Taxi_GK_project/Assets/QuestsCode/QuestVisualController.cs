using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestVisualController : MonoBehaviour
{
    [SerializeField] private Material questVisibleMaterial;
    [SerializeField] private GameObject questVisual;
    [SerializeField] private QuestVisualCollider questCollider;
    [SerializeField] private string materialColorName;
    [SerializeField] private int yRotationMesh;

    private Transform questVisualTransform;
    private MeshRenderer questVisualMeshRenderer;

    public bool IsUseNow = false;

    public void Init(Color visualColor)
    {
        questVisualTransform = questVisual.GetComponent<Transform>();
        questVisualMeshRenderer = questVisualMeshRenderer.GetComponent<MeshRenderer>();

        questVisualMeshRenderer.material = new Material(questVisibleMaterial);
        questVisualMeshRenderer.material.SetColor(materialColorName, visualColor);

        questCollider.Init(onCarInQuestArea);
    }

    private void Update()
    {
        questVisualTransform.Rotate(0, yRotationMesh, 0);
    }

    private void onCarInQuestArea()
    {

    }

    public Vector3 GetVisualPosition()
    {
        return questVisualTransform.position;
    }
}
