using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform creditsRectTransform; // Reference to the RectTransform of the credits panel
    public float slideDuration = 1.0f; // Duration of the slide animation
    private Vector3 originalPosition; // Store the original position of the credits panel

    private void Start()
    {
        // Save the original position of the credits panel
        if (creditsRectTransform != null)
        {
            originalPosition = creditsRectTransform.anchoredPosition;
        }
        else
        {
            Debug.LogError("Credits RectTransform is not assigned!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse hovered over the button!");
        PlayHoverEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited the button!");
        ReverseHoverEffect();
    }

    private void PlayHoverEffect()
    {
        if (creditsRectTransform == null)
        {
            Debug.LogError("Credits RectTransform is not assigned!");
            return;
        }

        // Move the credits panel to the center of the screen using LeanTween
        LeanTween.move(creditsRectTransform, Vector3.zero, slideDuration).setEase(LeanTweenType.easeInOutQuad);
    }

    private void ReverseHoverEffect()
    {
        if (creditsRectTransform == null)
        {
            Debug.LogError("Credits RectTransform is not assigned!");
            return;
        }

        // Move the credits panel back to its original position using LeanTween
        LeanTween.move(creditsRectTransform, originalPosition, slideDuration).setEase(LeanTweenType.easeInOutQuad);
    }
}