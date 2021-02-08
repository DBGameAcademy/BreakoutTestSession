using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    const string SCORE = "SCORE";
    const string NAME = "NAME";

    public Text ScoreText;

    private void OnEnable()
    {
        PopulateScores();
    }

    void PopulateScores()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("HIGH SCORES");
        sb.AppendLine("");
        for (int i = 5; i >= 1; i--)
        {
            sb.AppendLine(PlayerPrefs.GetInt(SCORE + i, i * 10000).ToString()); 
        }
        ScoreText.text = sb.ToString();
    }
}
