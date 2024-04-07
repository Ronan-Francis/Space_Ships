using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool staysWithinCameraBounds = true; // Toggle this in the Unity Editor

    void Start()
    {
        if (staysWithinCameraBounds)
        {
            // Adjust the position to ensure it's within screen bounds
            transform.position = GetAdjustedPositionWithinScreenBounds(transform.position);
        }
    }

    // This method is called when an object collides with the coin.
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with the coin is the player.
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(1); // Increment score by 1
            Destroy(gameObject); // Destroy the coin object
            Debug.Log("Coin Collected");
        }
    }

    Vector3 GetAdjustedPositionWithinScreenBounds(Vector3 originalPosition)
    {
        var camera = Camera.main;
        var screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        var adjustedPosition = originalPosition;

        // Check and adjust the x position
        if (originalPosition.x > screenBounds.x)
            adjustedPosition.x = screenBounds.x;
        else if (originalPosition.x < -screenBounds.x)
            adjustedPosition.x = -screenBounds.x;

        // Check and adjust the y position
        if (originalPosition.y > screenBounds.y)
            adjustedPosition.y = screenBounds.y;
        else if (originalPosition.y < -screenBounds.y)
            adjustedPosition.y = -screenBounds.y;

        return adjustedPosition;
    }
}
