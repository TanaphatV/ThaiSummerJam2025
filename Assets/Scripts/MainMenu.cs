using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject creditsMenuUI;

    public void PlayGame()
    {
        Debug.Log("Play Game button clicked");
        // Load the game scene here
    }

    public void Options()
    {
        Debug.Log("Options button clicked");
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void Credits()
    {
        Debug.Log("Credits button clicked");
        mainMenuUI.SetActive(false);
        creditsMenuUI.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu button clicked");
        optionsMenuUI.SetActive(false);
        creditsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked");
        Application.Quit();
    }
}
