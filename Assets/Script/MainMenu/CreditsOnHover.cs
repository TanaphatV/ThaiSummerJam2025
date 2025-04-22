using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsOnHover : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse hovered over the button!");
        PlayHoverEffect();
    }

    private void PlayHoverEffect()
    {
        // Add your hover effect logic here (e.g., play a sound, change color, etc.)
        Debug.Log("Hover effect triggered!");
    }
}