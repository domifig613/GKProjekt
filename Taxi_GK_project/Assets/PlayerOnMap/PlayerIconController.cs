using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera mapCamera;
    [SerializeField] Image playerIcon;
    [SerializeField] Image mapCanvas;

    void Start()
    {
        playerIcon = Instantiate(playerIcon);
    }
    
    public void SetNewPlayer(Transform player)
    {
        this.player = player;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 screenPos = mapCamera.WorldToViewportPoint(player.position);
            playerIcon.transform.SetParent(transform.parent);

            RectTransform rt = mapCanvas.GetComponent<RectTransform>();

            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            screenPos.x = rt.rect.width * screenPos.x + corners[0].x;
            screenPos.y = rt.rect.height * screenPos.y + corners[0].y;
            screenPos.z = 0;

            playerIcon.transform.position = screenPos;
            playerIcon.transform.SetAsLastSibling();
        }
    }
}
