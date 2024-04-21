using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Include this if using TextMeshPro

public class PlayerIsKilledOnCollision : MonoBehaviour
{
    public float delayBeforeLoading = 2.0f; // Delay before loading the scene
    public GameObject textPrefab; // Assign this in the inspector
    public GameObject canvasGameObject; // Assign the canvas GameObject in the inspector
    public Vector2 textPosition = new Vector2(-50, -50); // Position to place the text

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowDeathText();
            StartCoroutine(DelaySceneLoadAndDestroyPlayer(collision.gameObject));
        }
    }

    void ShowDeathText()
    {
        if (textPrefab != null && canvasGameObject != null)
        {
            // Instantiate the text prefab at the origin
            GameObject textInstance = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, canvasGameObject.transform);
            textInstance.SetActive(true); // Ensure it's active

            // Adjust the local position of the text according to specified coordinates
            RectTransform rectTransform = textInstance.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = textPosition;
            }
            else
            {
                Debug.LogError("Expected a RectTransform on the text prefab.");
            }
        }
        else
        {
            Debug.LogError("Canvas GameObject or Text Prefab is not assigned.");
        }
    }

    IEnumerator DelaySceneLoadAndDestroyPlayer(GameObject player)
    {
        // Optional: Add any pre-destroy effects here
        Destroy(player); // Destroy the player object
        yield return new WaitForSeconds(delayBeforeLoading); // Wait for the specified delay
        SceneManager.LoadScene(0); // Load the main menu scene using its build index, 0 in this case
    }
}
