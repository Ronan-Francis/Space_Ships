using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        // Ensure there's only one instance of GameController.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // Keep the GameController across scenes.
    }

    // Call this method to start the game, e.g., from a UI button.
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame"); // Load the main game scene.
    }

    // Call this method to handle the game over.
    public void GameOver()
    {
        // Here you could display a game over screen and wait for player input to restart.
        Debug.Log("Game Over!");
        RestartGame();
    }

    // Call this to restart the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene.
    }
}
