using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public List<Transform> firePoints; // List of points from which bullets are fired
    public GameObject bulletPrefab; // The bullet prefab to be fired
    public float fireRate = 1f; // Time in seconds between each shot

    private float nextFireTime = 0f; // Time until the next fire
    public Camera mainCamera; // Main camera to check visibility

    void Start()
    {
        
    }

    void Update()
    {
        // Check if the current time is past the next fire time
        if (Time.time >= nextFireTime)
        {
            // Check if the gun is visible by the camera
            if (IsVisibleToCamera())
            {
                Fire();
                nextFireTime = Time.time + fireRate; // Reset the next fire time
            }
        }
    }

    private void Fire()
    {
        foreach (Transform firePoint in firePoints) // Loop through all fire points
        {
            if (bulletPrefab && firePoint)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                newBullet.transform.Rotate(0, 0, -90); // Adjust bullet rotation if necessary
            }
        }
    }

    private bool IsVisibleToCamera()
    {
        // Check if the renderer is within the camera's view frustum
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            if (GeometryUtility.TestPlanesAABB(planes, rend.bounds))
                return true;
        }
        return false;
    }
}
