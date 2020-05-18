using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestVisualController : MonoBehaviour
{
    [SerializeField] private Material questVisibleMaterial;
    [SerializeField] private GameObject questVisual;
    [SerializeField] private VisualCollider questCollider;
    [SerializeField] private string materialColorName;
    [SerializeField] private int yRotationMesh;

    private Transform questVisualTransform;
    private MeshRenderer questVisualMeshRenderer;
    private Action onCarEnter;

    public bool IsUseNow = false;

    public void Init()
    {
        questVisualTransform = questVisual.GetComponent<Transform>();
        questVisualMeshRenderer = questVisual.GetComponent<MeshRenderer>();
        questVisualMeshRenderer.material = new Material(questVisibleMaterial);
    }
        
    public void StartQuest(Color visualColor, Action onCarEnter)
    {
        gameObject.SetActive(true);
        questVisualMeshRenderer.material.SetColor("_RimColor", visualColor);
        this.onCarEnter = onCarEnter;

        questCollider.Init(onCarInQuestArea);
        IsUseNow = true;
    }

    private void Update()
    {
        if (IsUseNow)
        {
            questVisualTransform.Rotate(0, yRotationMesh, 0);
        }
    }

    private void onCarInQuestArea()
    {
        onCarEnter();
    }

    public void DisableQuestVisual()
    {
        IsUseNow = false;
        gameObject.SetActive(false);
    }

    public Vector3 GetVisualPosition()
    {
        return questVisualTransform.position;
    }
}
