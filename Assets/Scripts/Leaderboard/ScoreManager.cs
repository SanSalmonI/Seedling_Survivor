using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour 
{
    [SerializeField]
    private TextMeshProUGUI imputScore;
    public int score;
    [SerializeField]
    private TMP_InputField imputname;

    public UnityEvent<string, int> submitScoreEvent;


    public void SubmitScore()
    {
       
        imputScore.text = score.ToString();
        Debug.Log("Score Is: " + score);
        Debug.Log("imput score is " + imputScore.text);
       submitScoreEvent.Invoke(imputname.text, score );
    }
}
