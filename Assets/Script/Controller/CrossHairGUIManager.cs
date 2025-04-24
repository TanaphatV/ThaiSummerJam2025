using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairGUIManager : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private bool isLockedMouse;

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite empoweredSprite;
    [SerializeField] private Image crossHairImg;

    [SerializeField] private GameObject powerBarGFX;
    [SerializeField] private Image fillbar;

    private Coroutine crt;

    public Animator anim;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        LockedCursorMode(isLockedMouse);

        
    }

    void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out mousePos
        );

        rectTransform.anchoredPosition = mousePos;
    }

    [ContextMenu("test")]
    private void TestFunc()
    {
        ActivatePowerMode(10f);
    }

    public void Hit()
    {
        anim.Play("Hit");
    }

    private void LockedCursorMode(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ActivatePowerMode(float duration)
    {
        if (crt != null)
            StopCoroutine(crt);

        crt = StartCoroutine(DrainPower(duration));
    }

    private IEnumerator DrainPower(float duration)
    {
        float elapsed = 0f;
        powerBarGFX.SetActive(true);
        crossHairImg.sprite = empoweredSprite;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float remaining = Mathf.Clamp01(1f - (elapsed / duration));
            fillbar.fillAmount = remaining;

            yield return null;
        }

        fillbar.fillAmount = 0f;
        crossHairImg.sprite = normalSprite;
        powerBarGFX.SetActive(false);
    }
}
