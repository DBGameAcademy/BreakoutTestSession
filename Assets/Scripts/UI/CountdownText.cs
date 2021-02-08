using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour
{
    public Text DialogueText;

    public void BeginCountdown(int ticks = 3)
    {
        StartCoroutine(Countdown(ticks));
    }

    private IEnumerator Countdown(int ticks)
    {
        while (ticks > 0)
        {
            DialogueText.text = ticks.ToString();
            yield return new WaitForSeconds(0.5f);
            ticks--;
        }

        DialogueText.text = "GO!";
        yield return new WaitForSeconds(1);
        DialogueText.text = "";

        GameController.Instance.GoToGameState(GameController.eGameState.Play);
    }
}
