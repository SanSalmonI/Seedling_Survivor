using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Setup")]
    public GameObject heartPrefab;         // The Heart prefab (UI Image)
    public Transform heartsParent;         // The container (e.g., HeartsContainer) to hold hearts

    [Header("Lives")]
    public int maxLives = 5;
    public int currentLives = 5;

    private List<GameObject> heartList = new List<GameObject>();

    void Start()
    {
        // Initialize hearts
        CreateHearts(maxLives);
        UpdateHearts(currentLives);
    }

    // Dynamically create heart objects for the maximum possible lives
    void CreateHearts(int numHearts)
    {
        for (int i = 0; i < numHearts; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);
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
