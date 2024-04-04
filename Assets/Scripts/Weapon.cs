using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public WeaponType weaponType;
    private Animator animator; // Animator reference

    void Awake()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    // Method to update the 'isFiring' boolean in the Animator
    public void SetFiring(bool isFiring)
    {
        if (animator != null)
        {
            animator.SetBool("isFiring", isFiring);
        }
    }
}
