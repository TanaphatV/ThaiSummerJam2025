using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCanvas : MonoBehaviour
{
    public GameObject parentFromCanvas;
    public float rotationDuration = 1.0f; // Duration of the rotation in seconds
    private bool isRotating = false;

    public void RotateCanvasOut()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCanvasCoroutine());
        }
    }

    public void RotateCanvasBackIn()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCanvasBackCoroutine());
        }
    }

    private IEnumerator RotateCanvasCoroutine()
    {
        isRotating = true;
        float elapsedTime = 0f;
        float startRotation = 0f;
        float endRotation = 90f;

        while (elapsedTime < rotationDuration)
        {
            float currentRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            parentFromCanvas.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly 90 degrees
        parentFromCanvas.transform.rotation = Quaternion.Euler(0, endRotation, 0);
        isRotating = false;
    }


    private IEnumerator RotateCanvasBackCoroutine()
    {
        isRotating = true;
        float elapsedTime = 0f;
        float startRotation = 90f;
        float endRotation = 0f;

        while (elapsedTime < rotationDuration)
        {
            float currentRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            parentFromCanvas.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly 0 degrees
        parentFromCanvas.transform.rotation = Quaternion.Euler(0, endRotation, 0);
        isRotating = false;
    }
}
