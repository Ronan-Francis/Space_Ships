using UnityEngine;

public class Engine : MonoBehaviour
{
    public float boostMultiplier = 2.0f; // The multiplier to apply to speed when boosting
    public GameObject[] upgradeImages; // Array of GameObjects that represent the upgrade images
    public GameObject boostEffect; // GameObject that represents the boost effect

    private float normalSpeed; // The normal movement speed
    private float boostedSpeed; // The speed when boosted
    public float speed = 5.0f; // Base speed of the player
    private int currentUpgradeIndex = 0; // Current index of the upgrade image being displayed
    private bool isBoosting = false; // Whether or not the boost is currently active
    private float boostDuration = 3.0f; // Initial boost duration in seconds
    private float boostRechargeTime = 5.0f; // Time it takes to fully recharge boost
    private float boostCooldown = 0; // Counter for the cooldown

    void Start()
    {
        normalSpeed = speed;
        boostedSpeed = speed * boostMultiplier;
        UpdateUpgradeImage(currentUpgradeIndex); // Display the initial upgrade image
    }

    void Update()
    {
        HandleBoostInput();
        UpdateBoostCooldown();
    }

    void HandleBoostInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && boostCooldown <= 0)
        {
            ApplyBoost();
        }
        if (Input.GetKey(KeyCode.LeftShift) && isBoosting)
        {
            if (boostDuration > 0)
            {
                boostDuration -= Time.deltaTime;
            }
            else
            {
                RemoveBoost();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RemoveBoost();
        }
    }

    void ApplyBoost()
    {
        isBoosting = true;
        speed = boostedSpeed;
        if (boostEffect != null) boostEffect.SetActive(true);
    }

    void RemoveBoost()
    {
        isBoosting = false;
        speed = normalSpeed;
        if (boostEffect != null) boostEffect.SetActive(false);
        if (boostDuration <= 0) // Only start cooldown if fully depleted
        {
            boostCooldown = boostRechargeTime;
            boostDuration = 0; // Ensure duration is not negative
        }
    }

    void UpdateBoostCooldown()
    {
        if (isBoosting) return; // Don't recharge while boosting

        if (boostCooldown > 0)
        {
            boostCooldown -= Time.deltaTime;
            if (boostCooldown <= 0)
            {
                // Boost is fully recharged
                boostDuration = 3.0f + 0.5f * currentUpgradeIndex; // Increase duration based on upgrades
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Upgrade"))
        {
            UpgradeEngine();
            Destroy(other.gameObject);
        }
    }

    public void UpgradeEngine()
    {
        currentUpgradeIndex++;
        UpdateUpgradeImage(currentUpgradeIndex);
        // Optionally increase boost duration or recharge time directly here
    }

    private void UpdateUpgradeImage(int index)
    {
        for (int i = 0; i < upgradeImages.Length; i++)
        {
            if (upgradeImages[i] != null)
            {
                upgradeImages[i].SetActive(i == index % upgradeImages.Length);
            }
        }
    }
}
