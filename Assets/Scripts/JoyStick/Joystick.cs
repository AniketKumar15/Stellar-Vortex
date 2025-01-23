using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background; // Joystick background
    public RectTransform handle;     // Joystick handle
    public float handleRange = 100f; // Maximum distance handle can move from the center

    private Vector2 input = Vector2.zero; // Normalized input vector

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Start dragging immediately on touch
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        // Convert screen position to local position on the joystick
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out position);

        // Clamp position to the handle range
        position = Vector2.ClampMagnitude(position, handleRange);

        // Update handle's position and normalize input
        handle.anchoredPosition = position;
        input = position / handleRange; // Normalize between -1 and 1
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset joystick on release
        handle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
    }

    // Function to return joystick input for player movement
    public Vector2 GetInput()
    {
        return input;
    }
}
