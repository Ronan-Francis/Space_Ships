using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public string enemyTag = "Enemy";
    public float lifetime = 10.0f;  // Lifetime of the rocket in seconds

    private Transform target;
    private float lifetimeTimer;

    void Start()
    {
        lifetimeTimer = lifetime;
        transform.Rotate(0, 0, -90);
    }

    void Update()
    {
        FindNearestTarget();
        if (target != null)
        {
            RotateTowardsTarget();
            MoveTowardsTarget();
        }

        // Reduce the lifetime timer and destroy the rocket if time runs out
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float nearestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    void RotateTowardsTarget()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsTarget()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Destroy(gameObject);  // Destroy the rocket
            // Optionally, handle other effects like damage or explosions
        }
    }
}
