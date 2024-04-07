using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed
    public GameObject[] weaponPrefabs; // Array to hold the weapon prefabs
        public bool[] canSwitchTo; // Array indicating if the weapon can be switched to
    public int currentWeaponIndex = 0; // Index of the current weapon
    public Transform weaponsParent; // Parent transform to hold weapons as children

    private float minX, maxX, minY, maxY; // Screen bounds
    private GameObject currentWeaponInstance; // The instance of the current weapon attached to the player
    private Weapon currentWeaponScript; // Script reference to the current weapon

    void Start()
    {
        // Calculate screen bounds
        CalculateScreenBounds();

        // Initialize the first weapon
        EquipWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Update screen bounds (optional, only if your game needs dynamic camera resizing)
        CalculateScreenBounds();

        // Player movement
        MovePlayer();

        // Shooting - trigger or stop shooting based on input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentWeaponScript != null)
            {
                currentWeaponScript.SetFiring(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (currentWeaponScript != null)
            {
                currentWeaponScript.SetFiring(false);
            }
        }

        // Cycling through weapons
        CycleWeapons();
    }
    void CalculateScreenBounds()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        minX = -screenBounds.x;
        maxX = screenBounds.x;
        minY = -screenBounds.y;
        maxY = screenBounds.y;
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }

    void CycleWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Q key to cycle to the previous weapon
        {
            FindNextWeapon(-1);
        }
        if (Input.GetKeyDown(KeyCode.E)) // E key to cycle to the next weapon
        {
            FindNextWeapon(1);
        }
    }

    void FindNextWeapon(int direction)
    {
        int originalIndex = currentWeaponIndex;
        bool found = false;

        do
        {
            currentWeaponIndex += direction;
            if (currentWeaponIndex < 0) currentWeaponIndex = weaponPrefabs.Length - 1;
            else if (currentWeaponIndex >= weaponPrefabs.Length) currentWeaponIndex = 0;

            if (canSwitchTo[currentWeaponIndex])
            {
                found = true;
                EquipWeapon(currentWeaponIndex);
                break;
            }
        } while (currentWeaponIndex != originalIndex);

        if (!found)
        {
            // If no switchable weapon is found, reset to starting weapon and make it switchable
            canSwitchTo[currentWeaponIndex] = true; // Ensure the current (or any) weapon can be switched to
            EquipWeapon(currentWeaponIndex);
        }
    }

    void EquipWeapon(int index)
    {
        // Destroy the old weapon instance if it exists
        if (currentWeaponInstance != null) Destroy(currentWeaponInstance);

        // Create a new weapon instance and set it as the current weapon
        GameObject weaponPrefab = weaponPrefabs[index];
        if (weaponPrefab != null)
        {
            currentWeaponInstance = Instantiate(weaponPrefab, Vector3.zero, Quaternion.Euler(0, 0, -90), weaponsParent);
            currentWeaponInstance.transform.localPosition = Vector3.zero; // Position the weapon at the player's center
            currentWeaponScript = currentWeaponInstance.GetComponent<Weapon>(); // Get the Weapon script
        }
    }
}
