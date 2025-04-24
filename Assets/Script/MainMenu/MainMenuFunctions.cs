using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuFunctions : MonoBehaviour
{
    public Button playButton;
    public TextMeshProUGUI lastScore;
    public TextMeshProUGUI highScore;

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

    public void UpdateScoreText(int currentScore)
    {
        lastScore.text = "Last Run: " + currentScore.ToString() + " pts";
        highScore.text = "HighScore: " + PlayerPrefs.GetInt(PlayerPrefKey.HIGH_SCORE,0) + " pts";
    }


    public void PlayGame()
    {
        Debug.Log("Play Game button clicked");
        RotateCanvasOut();
        // Load the game scene here
        GameManager.instance.StartGame();
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

    public void RotateCanvasBackIn()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCanvasBackCoroutine());
        }
    }

    private IEnumerator RotateCanvasCoroutine()
    {
        playButton.enabled = false;
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
        playButton.enabled = true;
    }


    private IEnumerator RotateCanvasBackCoroutine()
    {
        playButton.enabled = false;
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
        playButton.enabled = true;
    }
}
