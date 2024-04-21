using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab to shoot
    public WeaponType weaponType; // The type of weapon, used to determine shooting behavior
    private Animator animator; // Animator reference

    void Awake()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    public void SetFiring(bool isFiring)
    {
        if (animator != null)
        {
            animator.SetBool("isFiring", isFiring);
        }

        if (isFiring)
        {
            Shoot(); // Call the appropriate shooting function based on the weapon type
        }
    }

    private void Shoot()
    {
        switch (weaponType)
        {
            case WeaponType.AutoCannon:
                ShootAutoCannon();
                break;
            case WeaponType.BigSpaceGun:
                ShootBigSpaceGun();
                break;
            case WeaponType.Rockets:
                ShootRockets();
                break;
            case WeaponType.Zapper:
                ShootZapper();
                break;
                // Add cases for other weapon types as needed
        }
    }

    private void ShootAutoCannon()
    {
        // Instantiate bullet for auto cannon
        // Assuming two fire points for demonstration
        Transform firepoint1 = transform.GetChild(0);
        Transform firepoint2 = transform.GetChild(1);
        Instantiate(bulletPrefab, firepoint1.position, firepoint1.rotation);
        Instantiate(bulletPrefab, firepoint2.position, firepoint2.rotation);
    }

    private void ShootBigSpaceGun()
    {
        // Instantiate bullet for big space gun
        Transform firepoint = transform.GetChild(0); // Assuming a single fire point
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }

    private void ShootRockets()
    {
        // Instantiate rockets
        Transform firepoint1 = transform.GetChild(0);
        Transform firepoint2 = transform.GetChild(1);
        Instantiate(bulletPrefab, firepoint1.position, firepoint1.rotation);
        Instantiate(bulletPrefab, firepoint2.position, firepoint2.rotation);
    }

    private void ShootZapper()
    {
        // Instantiate zapper shot
        Transform firepoint = transform.GetChild(0); // Assuming a single fire point
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}
