using UnityEngine;

public class FollowAgent : MonoBehaviour
{
    [Header("AI Settings")]
    public GameObject Target;
    public float FollowSpeed = 2f;

    void Update()
    {
        // Move towards the target
        transform.position = Vector2.MoveTowards(
            transform.position,
            Target.transform.position,
            FollowSpeed * Time.deltaTime
        );

        // Rotate to face the target
        // 1. Calculate direction from agent to target
        Vector2 direction = (Vector2)Target.transform.position - (Vector2)transform.position;

        // 2. Calculate the angle in degrees, adjusting so the "forward" is upward
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // 3. Apply the rotation around the Z-axis (2D forward)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
