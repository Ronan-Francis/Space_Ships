using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List to hold the enemy prefabs
    public float spawnRate = 2f;          // Rate at which enemies are spawned
    public float movementSpeed = 3f;      // Speed at which the spawner moves
    public float maxY = 5f;               // Maximum height
    public float minY = -5f;              // Minimum height
    private float direction = 1f;         // Direction of movement, 1 is up, -1 is down
    private float nextSpawnTime;          // Time after which to spawn the next enemy

    void Update()
    {
        // Handle spawner movement
        MoveSpawner();

        // Handle enemy spawning
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void MoveSpawner()
    {
        // Move the spawner up and down
        float newY = transform.position.y + direction * movementSpeed * Time.deltaTime;
        if (newY > maxY || newY < minY)
        {
            direction *= -1; // Change direction if reaching the limits
        }
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0)
            return;

        // Choose a random enemy prefab
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
