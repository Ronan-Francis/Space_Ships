using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float maxShieldStrength = 10f; // Maximum strength of the shield
    public float regenRateVisible = 5f; // Rate at which the shield regenerates per second when visible
    public float regenRateInvisible = 10f; // Rate at which the shield regenerates per second when not visible
    public float currentShieldStrength; // Current strength of the shield

    private SpriteRenderer spriteRenderer;
    private bool shieldActive = true; // Initial state of the shield

    private void Start()
    {
        currentShieldStrength = maxShieldStrength; // Initialize shield strength
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateShieldVisibility(); // Set initial visibility based on shield state
    }

    void Update()
    {
        HandleCapsLockInput();
        RegenerateShield();
        UpdateShieldVisibility();
        Debug.Log("Shield Strength: " + currentShieldStrength);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((shieldActive && collision.CompareTag("Enemy")))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if ((bullet != null) && (spriteRenderer.enabled))
            {
                DepleteShield(10);
                Destroy(collision.gameObject);
            }
        }
    }

    void DepleteShield(float damage)
    {
        currentShieldStrength -= damage;
        if (currentShieldStrength < 0)
            currentShieldStrength = 0; // Ensure shield strength does not go negative
    }

    void RegenerateShield()
    {
        if (shieldActive && currentShieldStrength < maxShieldStrength)
        {
            float effectiveRegenRate = spriteRenderer.enabled ? regenRateVisible : regenRateInvisible;
            currentShieldStrength += effectiveRegenRate * Time.deltaTime;
            currentShieldStrength = Mathf.Min(currentShieldStrength, maxShieldStrength);
        }
    }

    void UpdateShieldVisibility()
    {
        spriteRenderer.enabled = currentShieldStrength > 1 && shieldActive;
    }

    void HandleCapsLockInput()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            shieldActive = !shieldActive; // Toggle shield active state
            Debug.Log("Shield Active: " + shieldActive);
        }
    }

    public void HandleButtonInput()
    {
        shieldActive = !shieldActive; // Toggle the shield's active state on button press
        Debug.Log("Shield toggled. Current state: " + (shieldActive ? "Active" : "Inactive"));
    }
}
