using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("UI References (TMP)")]
    public TextMeshProUGUI scoreText;

    [Header("Player Stats")]
    public List<GameObject> lives = new List<GameObject>();
    public int score = 0;

    private float timeElapsed = 0f;

    private void Update()
    {
        // Increment time elapsed
        timeElapsed += Time.deltaTime;

        // Increase score by 10 every second
        if (timeElapsed >= 0.1f)
        {
            IncreaseScore(1);
            timeElapsed = 0f; // Reset the timer
        }

        // Update UI text fields
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void DecreaseLife()
    {
        if (lives.Count > 0)
        {
            GameObject life = lives[0];
            life.SetActive(false); // Disable the top life object
            lives.RemoveAt(0); // Remove it from the list
        }
    }
}