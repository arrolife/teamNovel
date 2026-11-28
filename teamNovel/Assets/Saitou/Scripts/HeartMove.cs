using UnityEngine;
using UnityEngine.EventSystems;

public class HeartMove : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private RectTransform parentRect;
    private Vector2 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRect = (RectTransform)rectTransform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        offset = rectTransform.anchoredPosition - localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        rectTransform.anchoredPosition = localPoint + offset;
    }
}