using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    public GameObject parentFromCanvas;
    public float rotationDuration = 1.0f; // Duration of the rotation in seconds
    private bool isRotating = false;

    public void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar pressed");
        }
    }


    public void PlayGame()
    {
        Debug.Log("Play Game button clicked");
        RotateCanvasOut();
        // Load the game scene here
    }

    public void ThailandGameJamURL()
    {
        Debug.Log("Thailand Game Jam URL button clicked");
        Application.OpenURL("https://itch.io/jam/thailand-summer-jam-2025");
    }

    public void RotateCanvasOut()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCanvasCoroutine());
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
}
