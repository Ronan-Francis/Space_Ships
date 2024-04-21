using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class MenuScript : MonoBehaviour
{
    // Method to load a game scene
    public void PlayGame()
    {
        // Load the scene with index 1 (typically the first gameplay scene)
        SceneManager.LoadScene(1);
    }

    // Method to load the options menu
    public void OpenOptions()
    {
        Debug.Log("Quit game");
    }

    // Method to go back to the main menu
    public void ReturnToMainMenu()
    {
        // Load the scene with index 0 (typically the main menu scene)
        SceneManager.LoadScene(0);
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit game");
        Application.Quit();
    }
}
