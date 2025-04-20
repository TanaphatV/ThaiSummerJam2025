using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play Game button clicked");
        // Load the game scene here
    }

    public void ThailandGameJamURL()
    {
        Debug.Log("Thailand Game Jam URL button clicked");
        Application.OpenURL("https://itch.io/jam/thailand-summer-jam-2025");
    }
}
