using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float delayInSeconds = 2.0f;  // Time to wait before loading the next level
    public GameObject textBox;  // Drag the text box from your scene to this field in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateTextBox();
            StartCoroutine(LoadNextLevelAfterDelay());
        }
    }

    void ActivateTextBox()
    {
        if (textBox != null)
        {
            textBox.SetActive(true);  // Activate the text box
        }
    }

    IEnumerator LoadNextLevelAfterDelay()
    {
        // Optionally turn off the sprite renderer or other actions
        GetComponent<SpriteRenderer>().enabled = false;

        // Wait for the specified number of seconds
        yield return new WaitForSeconds(delayInSeconds);

        // Load the next level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop back to the first level if there are no more levels
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
