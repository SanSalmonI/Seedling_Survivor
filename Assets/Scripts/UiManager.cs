using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("UI References (TMP)")]
    public TextMeshProUGUI scoreText;
    public GameObject ScoreTextObject;
    public GameObject gameOverScreen;
    public TextMeshProUGUI FinalScore;
    // Reference to the Game Over screen

    [Header("Player Stats")]
   
    public int score = 0;
    private int finalScore = 0;

    // 1) Make this public so PlayerHealthUI can set it to true
    public bool isGameOver = false;

    private float timeElapsed = 0f;
    public ScoreManager scoreManager;
  
   

    private void Update()
    {
        if (!isGameOver)
        {
            // Update the timer
            timeElapsed += Time.deltaTime;

            // Increase score every 0.1 second
            if (timeElapsed >= 0.1f)
            {
                IncreaseScore(1);
                timeElapsed = 0f;
            }

         
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            finalScore = score;
            // Freeze the score at finalScore
            FinalScore.text = finalScore.ToString();
            gameOverScreen.SetActive(true);
            ScoreTextObject.SetActive(false);
            
            sendScoreToLeaderboard(finalScore);
           
        }
    }

    public void IncreaseScore(int amount)
    {
        if (!isGameOver)
        {
            score += amount;
            //Debug.Log("Score increased. Current score: " + score);
        }
      
    }

    public void sendScoreToLeaderboard(int score)
    {
        if (scoreManager != null)
        {
            scoreManager.score = score;
            //Debug.Log("sent Score to Leaderboard  " + score);
        }
        else
        {
            Debug.Log("couldnt find ScoreManager!");
        }
    }


private void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Debug.Log("Game Over screen displayed.");
            Debug.Log("Final score: " + finalScore);
        }
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
}
