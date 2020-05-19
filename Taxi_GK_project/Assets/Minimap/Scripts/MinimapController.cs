using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject
{
    public Image image { get; set; }
    public GameObject gameObject { get; set; }
}

public class MinimapController : MonoBehaviour
{
    public Transform player;
    public Camera minimapCamera;
    public Image questImage;
    public Image gasStationImage;
    public Color gasStationColor;
    public float maxDistance;
    public List<GameObject> gasStationList = new List<GameObject>();
    public List<GameObject> questsList = new List<GameObject>();
    private List<MapObject> gasStationOnMapList = new List<MapObject>();
    private List<MapObject> questsOnMapList = new List<MapObject>();

    private void DrawMapIcons()
    {
        foreach (var icon in gasStationOnMapList)
        {
            Vector2 playerPos = new Vector2(player.position.x, player.position.z);
            Vector2 iconPos = new Vector2(icon.gameObject.transform.position.x, icon.gameObject.transform.position.z);
            float distance = Vector2.Distance(playerPos, iconPos);
            print(playerPos + "        " + iconPos);
            if (distance > maxDistance)
            {
                icon.image.enabled = false;
                continue;
            }
            else
            {
                icon.image.enabled = true;
            }

            Vector3 screenPos = minimapCamera.WorldToViewportPoint(icon.gameObject.transform.position);
            icon.image.transform.SetParent(this.transform.parent);
            
            RectTransform rt = this.GetComponentInParent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            screenPos.x = screenPos.x * rt.rect.width * 2 + corners[0].x;
            screenPos.y = screenPos.y * rt.rect.height * 2 + corners[0].y;

            float r = rt.rect.height;
            Vector2 circleCentre = new Vector2(rt.rect.width + corners[0].x, rt.rect.height + corners[0].y);
            Vector2 xAxis = new Vector2(1f, 0f);
            Vector2 point = new Vector2(screenPos.x - circleCentre.x, screenPos.y - circleCentre.y);
            xAxis.Normalize();
            point.Normalize();
            float cos = Vector2.Dot(xAxis, point);
            float alfa = Mathf.Acos(cos);
            float sin = Mathf.Sin(alfa);
            Vector2 maxPos = new Vector2(cos * r, sin * r);
            maxPos.x = Mathf.Abs(maxPos.x);
            maxPos.y = Mathf.Abs(maxPos.y);

            screenPos.x = Mathf.Clamp(screenPos.x, circleCentre.x - maxPos.x, circleCentre.x + maxPos.x);
            screenPos.y = Mathf.Clamp(screenPos.y, circleCentre.y - maxPos.y, circleCentre.y + maxPos.y);
            screenPos.z = 0;

            icon.image.transform.position = screenPos;
            icon.image.transform.SetAsLastSibling();
        }
    }
    
    private void Start()
    {
        foreach(var gasStation in gasStationList)
        {
            Image icon = Instantiate(gasStationImage);
            icon.color = gasStationColor;
            gasStationOnMapList.Add(new MapObject
            {
                image = icon,
                gameObject = gasStation
            });
        }
        foreach (var quest in questsList)
        {
            Image icon = Instantiate(questImage);
            questsOnMapList.Add(new MapObject
            {
                image = icon,
                gameObject = quest
            });
        }
    }
    
    private void Update()
    {
        DrawMapIcons();
    }
}
