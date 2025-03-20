using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Setup")]
    public GameObject heartPrefab;         // The Heart prefab (UI Image)
    public Transform heartsParent;         // The container (e.g., HeartsContainer) to hold hearts
    public float heartSpacing = 50f;       // Spacing between each heart
    public GameObject gameOverScreen;      // Reference to the Game Over screen

    [Header("Lives")]
    public int maxLives = 5;
    public int currentLives = 5;

    private List<GameObject> heartList = new List<GameObject>();

    void Start()
    {
        // Initialize hearts
        CreateHearts(maxLives);
        UpdateHearts(currentLives);

        // Ensure the Game Over screen is disabled at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    // Dynamically create heart objects for the maximum possible lives
    void CreateHearts(int numHearts)
    {
        for (int i = 0; i < numHearts; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);
            RectTransform heartRect = newHeart.GetComponent<RectTransform>();

            // Position the heart to the right of the previous one
            if (i > 0)
            {
                RectTransform previousHeartRect = heartList[i - 1].GetComponent<RectTransform>();
                heartRect.anchoredPosition = previousHeartRect.anchoredPosition + new Vector2(heartSpacing, 0);
            }

            heartList.Add(newHeart);
        }
    }

    // Enable or disable hearts based on current lives
    void UpdateHearts(int lives)
    {
        // Make sure we don't exceed the number of hearts in the list
        for (int i = 0; i < heartList.Count; i++)
        {
            if (i < lives)
                heartList[i].SetActive(true);
            else
                heartList[i].SetActive(false);
        }

        // Check if the player has run out of lives
        if (lives <= 0 && gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    // Call this method to add or remove lives
    public void SetLives(int newLives)
    {
        currentLives = Mathf.Clamp(newLives, 0, maxLives);
        UpdateHearts(currentLives);
    }

    // Decrease life by 1
    public void LoseOneLife()
    {
        currentLives--;
        if (currentLives < 0) currentLives = 0;
        UpdateHearts(currentLives);
    }

    // Increase life by 1
    public void GainOneLife()
    {
        currentLives++;
        if (currentLives > maxLives) currentLives = maxLives;
        UpdateHearts(currentLives);
    }
}
