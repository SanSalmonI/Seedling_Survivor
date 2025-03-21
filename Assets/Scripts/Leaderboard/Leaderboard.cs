using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using Dan.Main;
using System.Linq;

public class Leaderboard : MonoBehaviour {
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "664a499490c6e160cd8575d6fda5b30fcb294928c6c99bd7ac8b6acf3109c809";

    private void Start()
    {
        getLeaderboard();
    }
    public void getLeaderboard() {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLenght = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLenght; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            Debug.Log("imputing score to leaderboard: " + score);
            getLeaderboard();
        }));
    }
}
