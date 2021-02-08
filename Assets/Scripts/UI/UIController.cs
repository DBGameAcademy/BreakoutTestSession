using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoSingleton<UIController>
{
    public GameObject MenuContainer;
    public GameObject UIContainer;

    public UIPlayerLives PlayerLives;

    public CountdownText CountdownText;

    public Text ScoreText;

    public void UpdateScore(int _score)
    {
        string scoreString = _score.ToString().PadLeft(6, '0');
        ScoreText.text = scoreString;
    }
}
