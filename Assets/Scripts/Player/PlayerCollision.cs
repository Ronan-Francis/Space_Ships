using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Assign this in the inspector
    public Sprite[] damageSprites; // Array of sprites to cycle through when damaged
    private int currentDamageIndex = 0; // Track the current damage level
    void Start()
    {
        // Get the SpriteRenderer attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("PlayerCollision: No SpriteRenderer found on the GameObject.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Example collision detection with an enemy or damaging object
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        // Increase the damage index and clamp it to the length of the array
        currentDamageIndex = Mathf.Min(currentDamageIndex + 1, damageSprites.Length - 1);

        // Change the sprite to reflect damage
        UpdateSprite();

        // If the damage index reaches the last sprite, you may want to handle player death
        if (currentDamageIndex >= damageSprites.Length - 1)
        {
            PlayerDeath();
        }
    }

    public void Repair()
    {
        // Decrease the damage index but never below zero
        currentDamageIndex = Mathf.Max(currentDamageIndex - 1, 0);

        // Change the sprite to reflect the repair
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        // Change the sprite to the current damage index
        spriteRenderer.sprite = damageSprites[currentDamageIndex];
    }

    private void PlayerDeath()
    {
        // Handle the player's death here
        Debug.Log("Player has been destroyed!");
        // For example, you could disable the player's movement and trigger a game over sequence
    }
}
