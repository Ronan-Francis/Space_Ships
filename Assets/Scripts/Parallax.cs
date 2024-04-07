using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffectMultiplier = 0.5f;
    public bool moveLeft = true;

    private float textureUnitSizeX;
    private Camera mainCamera;
    private Vector3 lastCameraPosition;

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraPosition = mainCamera.transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void Update()
    {
        Vector3 deltaMovement = mainCamera.transform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = mainCamera.transform.position;

        // Calculate the edges of the camera's viewport
        float cameraHorizontalExtend = mainCamera.orthographicSize * Screen.width / Screen.height;
        float cameraLeftEdge = mainCamera.transform.position.x - cameraHorizontalExtend;
        float cameraRightEdge = mainCamera.transform.position.x + cameraHorizontalExtend;

        // Check if the background has reached the camera's edge
        if ((moveLeft && transform.position.x < cameraLeftEdge) || (!moveLeft && transform.position.x > cameraRightEdge))
        {
            // Reverse the direction of the movement
            moveLeft = !moveLeft;
        }

        // Move the background
        if(moveLeft)
        {
            transform.Translate(Vector2.left * parallaxEffectMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * parallaxEffectMultiplier * Time.deltaTime);
        }
    }
}
