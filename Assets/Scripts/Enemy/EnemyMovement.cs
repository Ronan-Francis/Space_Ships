using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Speed at which the enemy moves
    public float movementRange = 10.0f; // Maximum distance from the starting point

    private Vector3 startPosition; // To remember the starting position of the enemy
    private bool movingRight = true; // To track the current direction of movement

    void Start()
    {
        startPosition = transform.position; // Initialize startPosition to the enemy's initial position
    }

    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        // Check the direction and make the enemy move
        if (movingRight)
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        // Check if the enemy has exceeded the movement range
        if (transform.position.y > startPosition.y + movementRange)
        {
            movingRight = false; // Change direction to left
        }
        else if (transform.position.y < startPosition.y - movementRange)
        {
            movingRight = true; // Change direction to right
        }
    }
}
