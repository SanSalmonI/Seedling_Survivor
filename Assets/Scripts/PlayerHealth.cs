using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Setup")]
    public GameObject heartPrefab;
    public Transform heartsParent;
    public float heartSpacing = 50f;
    public GameObject gameOverScreen;

    [Header("Lives")]
    public int maxLives = 5;
    public int currentLives = 5;

    public UIManager uiManager;

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

    void CreateHearts(int numHearts)
    {
        for (int i = 0; i < numHearts; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);
            RectTransform heartRect = newHeart.GetComponent<RectTransform>();

            if (i > 0)
            {
                RectTransform previousHeartRect = heartList[i - 1].GetComponent<RectTransform>();
                heartRect.anchoredPosition = previousHeartRect.anchoredPosition + new Vector2(heartSpacing, 0);
            }

            heartList.Add(newHeart);
        }
    }

    void UpdateHearts(int lives)
    {
        // Enable/disable each heart
        for (int i = 0; i < heartList.Count; i++)
        {
            heartList[i].SetActive(i < lives);
            
        }

        // If the player is out of lives, end the game
        if (lives <= 0 && gameOverScreen != null)
        {
            // 2) Tell UIManager that the game is over
            if (uiManager != null)
            {
                uiManager.isGameOver = true;
               // Debug.Log("PlayerHealthUI: Game Over. Notified UIManager.");
            }

            ShowGameOverScreen();
        }
    }

    public void SetLives(int newLives)
    {
        currentLives = Mathf.Clamp(newLives, 0, maxLives);
        UpdateHearts(currentLives);
    }

    public void LoseOneLife()
    {
        currentLives--;
        if (currentLives < 0) currentLives = 0;
        UpdateHearts(currentLives);
    }

    public void GainOneLife()
    {
        currentLives++;
        if (currentLives > maxLives) currentLives = maxLives;
        UpdateHearts(currentLives);
    }

    public void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            //Debug.Log("PlayerHealthUI: Game Over screen displayed.");
           
        }
    }
}
