using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float driftSpeed = 0.5f; // Speed at which the asteroid drifts
    public float maxOffset = 5f; // Maximum offset from the spawn location

    private Vector2 spawnLocation;
    private Vector2 driftDirection;
    private Animator animator;
    public float animationDuration = 1f;
    public GameObject orePrefab; // Reference to the ore prefab
    public int oreDropCount = 3; // Number of ores to drop

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Animator component missing!");

        spawnLocation = transform.position; // Store the spawn location
        driftDirection = Random.insideUnitCircle.normalized; // Random drift direction
    }

    void Update()
    {
        Drift();
    }

    void Drift()
    {
        // Calculate the new position with drift
        Vector2 newPosition = (Vector2)transform.position + (driftDirection * driftSpeed * Time.deltaTime);

        // Check if the asteroid would exceed the max offset from its spawn location
        if ((newPosition - spawnLocation).magnitude > maxOffset)
        {
            // Invert the drift direction if the max offset is exceeded
            driftDirection = -driftDirection;
        }
        else
        {
            // Apply the new position if within max offset
            transform.position = newPosition;
        }
    }

    public void DestroyAsteroid()
    {
        animator.SetBool("PlayAnimation", true);
        DropOre();
        Destroy(gameObject, animationDuration);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            DestroyAsteroid();
        }
    }
    void DropOre()
    {
        for (int i = 0; i < oreDropCount; i++)
        {
            // Instantiate ore at the asteroid's position with a random offset
            Vector3 position = transform.position + Random.insideUnitSphere * 0.5f; // Adjust the multiplier for desired spread
            Instantiate(orePrefab, position, Quaternion.identity);
        }
    }
}
