using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5.0f;  // Speed at which the GameObject will move
    public bool moveRight = true;  // Boolean value to control direction

    void Update()
    {
        if (moveRight)
        {
            // Move the GameObject to the right
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            // Move the GameObject to the left
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }
}
