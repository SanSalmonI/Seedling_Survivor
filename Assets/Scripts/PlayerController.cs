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

    void Awake()
    {
        // Get and cache the Rigidbody2D
        rb2d = GetComponent<Rigidbody2D>();
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
            // By default, "Horizontal" uses A/D or Left/Right,
            // and "Vertical" uses W/S or Up/Down.
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
}
