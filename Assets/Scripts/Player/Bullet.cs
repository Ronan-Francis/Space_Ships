using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 screenBounds;

    void Start()
    {
        // Get the screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GetComponent<Rigidbody2D>().velocity = transform.up * speed; // Assumes bullet faces up
    }

    void Update()
    {
        // Check if the bullet is outside the screen bounds
        if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x ||
            transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
        {
            //Destroy(gameObject); // Destroy the bullet
        }
    }
}
