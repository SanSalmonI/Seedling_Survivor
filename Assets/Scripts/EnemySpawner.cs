using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to instantiate
    public Transform playerTransform; // Reference to the player's transform
    public float spawnRadius = 5f; // Radius within which enemies will spawn
    public float baseSpawnInterval = 2f; // Base interval between spawns
    public float spawnIntervalDecrease = 0.1f; // Decrease in spawn interval per score increment
    public int scoreIncrement = 1000; // Score increment for adjusting spawn interval

    private UIManager uiManager; // Reference to the UIManager
    private float spawnTimer = 0f;
    private float currentSpawnInterval;

    [System.Obsolete]
    void Start()
    {
        // Find the UIManager in the scene
        uiManager = FindObjectOfType<UIManager>();
        // Initialize the current spawn interval
        currentSpawnInterval = baseSpawnInterval;
    }

    void Update()
    {
        // Decrease the spawn timer
        spawnTimer -= Time.deltaTime;

        // Check if it's time to spawn an enemy
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            // Reset the spawn timer
            spawnTimer = currentSpawnInterval;
        }

        // Adjust the spawn interval based on the score
        currentSpawnInterval = Mathf.Max(baseSpawnInterval - (uiManager.score / scoreIncrement) * spawnIntervalDecrease, 0.5f);
    }

    void SpawnEnemy()
    {
        // Generate a random point within the unit circle
        Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;

        // Calculate the spawn position relative to the player's position
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomPoint.x, randomPoint.y, 0);

        // Instantiate the enemy prefab at the spawn position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Set the target for the enemy
        FollowAgent followAgent = enemy.GetComponent<FollowAgent>();
        if (followAgent != null)
        {
            followAgent.Target = playerTransform.gameObject;
        }
    }
}
