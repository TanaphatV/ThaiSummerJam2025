using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonJuicer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.2f; // Scale factor when hovering
    public float animationDuration = 0.2f; // Duration of the scaling animation

    private Vector3 originalScale; // Store the original scale of the button

    private void Start()
    {
        // Save the original scale of the button
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up the button when the mouse hovers over it
        LeanTween.scale(gameObject, originalScale * hoverScale, animationDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale back to the original size when the mouse exits
        LeanTween.scale(gameObject, originalScale, animationDuration).setEase(LeanTweenType.easeOutQuad);
    }
}