using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapController : MonoBehaviour, IPointerClickHandler
{
    public Texture2D worldMap;
    public float mapWidth = 360f;
    public float mapHeight = 180f;
    public GameObject pointer;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localCursor))
        {
            float xRatio = (localCursor.x + rectTransform.rect.width * 0.5f) / rectTransform.rect.width;
            float yRatio = (localCursor.y + rectTransform.rect.height * 0.5f) / rectTransform.rect.height;

            float longitude = xRatio * mapWidth - mapWidth * 0.5f;
            float latitude = yRatio * mapHeight - mapHeight * 0.5f;

            pointer.transform.position = new Vector3(localCursor.x, localCursor.y, 6f);

            Vector3 worldPoint = rectTransform.TransformPoint(localCursor);
            worldPoint.z = 6f;
            pointer.transform.position = worldPoint;

            PointerMove.instance.setCoordinates(latitude, longitude);

            Debug.Log($"Týklanan Koordinatlar: Enlem {latitude}, Boylam {longitude}");
        }
    }
}