using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject[] weaponPrefabs; // Array to hold the weapon prefabs
    public int currentWeaponIndex = 0; // Index of the current weapon
    public Transform weaponsParent; // Parent transform to hold weapons as children

    private float minX, maxX, minY, maxY;
    private GameObject currentWeaponInstance; // The instance of the current weapon attached to the ship
    private Weapon currentWeaponScript; // Script reference to the current weapon

    void Start()
    {
        // Calculate screen bounds
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        minX = -screenBounds.x;
        maxX = screenBounds.x;
        minY = -screenBounds.y;
        maxY = screenBounds.y;

        // Initialize the first weapon
        EquipWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            currentWeaponScript.SetFiring(false);
        }

        // Cycling through weapons
        if (Input.GetKeyDown(KeyCode.Q)) // Q key to cycle to the previous weapon
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0)
                currentWeaponIndex = weaponPrefabs.Length - 1;
            EquipWeapon(currentWeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.E)) // E key to cycle to the next weapon
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weaponPrefabs.Length;
            EquipWeapon(currentWeaponIndex);
        }
    }

    void Shoot()
    {
        if (currentWeaponScript != null && currentWeaponScript.bulletPrefab != null)
        {
            // Start firing
            currentWeaponScript.SetFiring(true);

            switch (currentWeaponScript.weaponType)
            {
                case WeaponType.AutoCannon:
                    // Logic for auto cannon shooting
                    ShootAutoCannon();

                    break;
                case WeaponType.BigSpaceGun:
                    // Logic for big space gun shooting
                    ShootBigSpaceGun();
                    break;
                case WeaponType.Rockets:
                    // Logic for rockets shooting
                    ShootRockets();
                    break;
                case WeaponType.Zapper:
                    // Logic for zapper shooting
                    ShootZapper();
                    break;
                    // Add cases for other weapon types
            }
        }
    }

    // Define methods for each shooting type
    void ShootAutoCannon()
    {
        // Instantiate bullet for auto cannon
        Transform firepoint1 = currentWeaponInstance.transform.GetChild(0);
        Transform firepoint2 = currentWeaponInstance.transform.GetChild(1);
        Instantiate(currentWeaponScript.bulletPrefab, firepoint1.position, firepoint1.rotation);
        Instantiate(currentWeaponScript.bulletPrefab, firepoint2.position, firepoint2.rotation);
    }

    void ShootBigSpaceGun()
    {
        // Instantiate bullet for big space gun
        Transform firepoint = currentWeaponInstance.transform.GetChild(0);
        Instantiate(currentWeaponScript.bulletPrefab, firepoint.position, firepoint.rotation);
        // Add any additional logic specific to big space gun
    }

    void ShootRockets()
    {
        // Instantiate rockets
        Transform firepoint = currentWeaponInstance.transform.GetChild(0);
        Instantiate(currentWeaponScript.bulletPrefab, firepoint.position, firepoint.rotation);
        // Add any additional logic specific to rockets
    }

    void ShootZapper()
    {
        // Instantiate zapper shot
        Transform firepoint = currentWeaponInstance.transform.GetChild(0);
        Instantiate(currentWeaponScript.bulletPrefab, firepoint.position, firepoint.rotation);
        // Add any additional logic specific to zapper
    }

    void EquipWeapon(int index)
    {
        // Destroy the old weapon instance if it exists
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance);
        }

        // Create a new weapon instance and set it as the current weapon
        GameObject weaponPrefab = weaponPrefabs[index];
        if (weaponPrefab != null)
        {
            currentWeaponInstance = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity, weaponsParent);
            currentWeaponInstance.transform.localPosition = Vector3.zero; // Position the weapon at the ship's center
            currentWeaponScript = currentWeaponInstance.GetComponent<Weapon>(); // Get the Weapon script
        }
    }
}
