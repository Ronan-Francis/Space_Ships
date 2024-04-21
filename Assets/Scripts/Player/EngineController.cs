using UnityEngine;

[System.Serializable] // This makes EngineUpgrade visible in the Unity Editor
public class EngineUpgrade
{
    public GameObject enginePrefab; // The engine upgrade prefab
    public GameObject boostEffectPrefab; // The associated boost effect prefab for this engine
}

public class EngineController : MonoBehaviour
{
    public EngineUpgrade[] upgrades; // Array of engine upgrades
    private GameObject currentEngineInstance; // The current engine instance
    private GameObject currentBoostEffectInstance; // The current boost effect instance
    private int currentUpgradeIndex = 0; // Index of the current upgrade
    public float speed = 5.0f; // Base speed
    public float boostMultiplier = 2.0f; // Boost speed multiplier
    private float normalSpeed;
    private float boostedSpeed;
    private bool isBoosting = false;
    private float boostDuration = 3.0f;
    private float boostRechargeTime = 5.0f;
    private float boostCooldown = 0;
    private Animator boostAnimator; // Animator for the boost effect
    public float engineEffectOffset = -1.0f; // 1 unit behind

    void Start()
    {
        normalSpeed = speed;
        boostedSpeed = speed * boostMultiplier;
        EquipEngine(currentUpgradeIndex); // Equip the initial engine
        boostAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleBoostInput();
        UpdateBoostCooldown();
    }

    void EquipEngine(int index)
    {
        // Destroy the old engine and boost effect instances if they exist
        if (currentEngineInstance != null) Destroy(currentEngineInstance);
        if (currentBoostEffectInstance != null) Destroy(currentBoostEffectInstance);

        // Get the specified upgrade
        EngineUpgrade upgrade = upgrades[index];

        // Instantiate the new engine prefab as a child of this GameObject
        currentEngineInstance = Instantiate(upgrade.enginePrefab, transform.position, Quaternion.Euler(0, 0, -90), transform);

        // Instantiate the new boost effect prefab as a child of this GameObject, but don't activate it yet
        if (upgrade.boostEffectPrefab != null)
        {
            Vector3 offset = transform.up    * engineEffectOffset;

            // Use the offset in the instantiation
            currentBoostEffectInstance = Instantiate(upgrade.boostEffectPrefab, transform.position + offset, Quaternion.Euler(0, 0, -90), transform);
        }
    }


    void HandleBoostInput()
    {
        // Example control logic, adjust as needed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartBoost();
            boostAnimator.SetBool("isBoosting", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopBoost();
            boostAnimator.SetBool("isBoosting", false);
        }

        // Update the Animator's isBoosting parameter to match the script's state
        
    }

    void StartBoost()
    {
        isBoosting = true;
        speed = boostedSpeed;
        if (currentBoostEffectInstance != null) currentBoostEffectInstance.SetActive(true);
    }

    void StopBoost()
    {
        isBoosting = false;
        speed = normalSpeed;
        if (currentBoostEffectInstance != null) currentBoostEffectInstance.SetActive(false);
        if (boostDuration <= 0) // Only start cooldown if fully depleted
        {
            boostCooldown = boostRechargeTime;
            boostDuration = 0;
        }
    }

    void UpdateBoostCooldown()
    {
        if (isBoosting) return;

        if (boostCooldown > 0)
        {
            boostCooldown -= Time.deltaTime;
            if (boostCooldown <= 0)
            {
                // Fully recharged
                boostDuration = 3.0f; // Reset duration for simplicity, could adjust based on upgrade
            }
        }
    }

    // Example upgrade method, could be called on specific game events
    public void UpgradeEngine()
    {
        currentUpgradeIndex = (currentUpgradeIndex + 1) % upgrades.Length;
        EquipEngine(currentUpgradeIndex);
    }
}
