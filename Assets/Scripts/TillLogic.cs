using UnityEngine;

public class Till : MonoBehaviour
{
    public enum TillState
    {
        Idle,
        Planted,
        Dead
    }

    [Header("Cooldown Settings")]
    [Tooltip("Cooldown duration in seconds when planted.")]
    public float plantedCooldown = 5f;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite plantedSprite;
    public Sprite deadSprite;

    [Header("Color Settings")]
    public Color fullCooldownColor = new Color(0f, 1f, 0f, 0.5f); // Green with 50% transparency
    public Color noCooldownColor = new Color(1f, 0f, 0f, 0.5f);   // Red with 50% transparency

    private TillState currentState = TillState.Idle;
    private float cooldownTimer = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set the initial sprite
        UpdateSprite();
    }

    void Update()
    {
        if (currentState == TillState.Planted)
        {
            // Decrease the cooldown timer
            cooldownTimer -= Time.deltaTime;

            // Update the color to indicate cooldown progress
            UpdateCooldownColor();

            // Check if the cooldown has ended
            if (cooldownTimer <= 0f)
            {
                currentState = TillState.Dead;
                // Update the sprite for the "dead" state
                UpdateSprite();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (currentState == TillState.Idle)
        {
            // Check if the collision is with an enemy
            if (collider.gameObject.CompareTag("Enemy"))
            {
                // Delete the enemy
                Destroy(collider.gameObject);

                // Change state to "planted"
                currentState = TillState.Planted;
                cooldownTimer = plantedCooldown;
                // Update the sprite for the "planted" state
                UpdateSprite();
            }
        }
        else if (currentState == TillState.Planted)
        {
            // Check if the collision is with the player
            if (collider.gameObject.CompareTag("Player"))
            {
                // Reset the cooldown timer
                cooldownTimer = plantedCooldown;
            }
        }
    }

    private void UpdateSprite()
    {
        // Update the sprite based on the current state
        switch (currentState)
        {
            case TillState.Idle:
                spriteRenderer.sprite = idleSprite;
                break;
            case TillState.Planted:
                spriteRenderer.sprite = plantedSprite;
                break;
            case TillState.Dead:
                spriteRenderer.sprite = deadSprite;
                break;
        }
    }

    private void UpdateCooldownColor()
    {
        // Interpolate the color based on the remaining cooldown time
        float t = Mathf.Clamp01(cooldownTimer / plantedCooldown);
        spriteRenderer.color = Color.Lerp(noCooldownColor, fullCooldownColor, t);
    }
}
