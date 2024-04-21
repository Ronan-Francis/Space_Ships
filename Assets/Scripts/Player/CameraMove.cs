using UnityEngine;
using UnityEngine.UI;          // For using the UI components
using UnityEngine.SceneManagement;  // For loading scenes
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public Transform target; // Assign the player's transform here in the inspector
    public float smoothSpeed = 0.0005f; // Adjust for smoother or more immediate follow
    public int nextSceneIndex; // Index of the scene to load after reaching the end position
    public Text endText; // Assign a UI Text element in the inspector to display the end message
    public float displayTime = 3f; // Time in seconds to display the end message before switching scenes

    private bool endReached = false; // Flag to check if the end has been reached

    void FixedUpdate()
    {
        if (target == null) return;

        // Determine the desired position based on target's position and offset
        Vector3 desiredPosition = target.position;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Check if the camera has reached the end position
        if (!endReached && Vector3.Distance(transform.position, target.position) <= 0)
        {
            endReached = true;
            StartCoroutine(EndSequence());
        }
    }

    IEnumerator EndSequence()
    {
        endText.enabled = true; // Enable the text to make it visible
        yield return new WaitForSeconds(displayTime); // Wait for specified display time
        SceneManager.LoadScene(nextSceneIndex); // Load the next scene by index
    }

}
