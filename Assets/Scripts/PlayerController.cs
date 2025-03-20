using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Movement speed of the player.")]
    public float moveSpeed = 5f;

    [Header("Freeze Settings")]
    [Tooltip("Till freeze in seconds.")]
    public float TillfreezeDuration = 2f;

    [Header("Prefab Settings")]
    [Tooltip("Prefab (Till)")]
    public GameObject prefabToSpawn;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb2d;
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    // Reference to the PlayerHealthUI
    private PlayerHealthUI playerHealthUI;

    [System.Obsolete]
    void Awake()
    {
        // Get and cache the Rigidbody2D
        rb2d = GetComponent<Rigidbody2D>();
        // Find the PlayerHealthUI in the scene
        playerHealthUI = FindObjectOfType<PlayerHealthUI>();
    }

    void Update()
    {
        if (isFrozen)
        {
            // Decrease the freeze timer
            freezeTimer -= Time.deltaTime;

            // Check if the freeze duration has ended
            if (freezeTimer <= 0f)
            {
                isFrozen = false;
                // Spawn the prefab at the player's position
                Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            }
        }
        else
        {
            // Gather input from WASD or arrow keys.
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            // Build the movement vector
            Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;

            // Apply velocity to the Rigidbody2D
            rb2d.linearVelocity = movement;

            // Check if the spacebar is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFrozen = true;
                freezeTimer = TillfreezeDuration;
                rb2d.linearVelocity = Vector2.zero; // Stop the player's movement
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collision is with an enemy
        if (collider.gameObject.CompareTag("Enemy"))
        {
            // Notify the PlayerHealthUI to lose a life
            playerHealthUI.LoseOneLife();

            // Optionally, destroy the enemy or handle the collision in another way
            Destroy(collider.gameObject);
        }
    }
}
