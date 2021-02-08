using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioObject PositiveMenuInput;

    void Update()
    {
        if (InputController.Instance.GetInput(InputController.InputAction.AnyKey) && !Fader.Instance.IsFading)
        {
            Debug.Log("Positive Audio :" + PositiveMenuInput);

            Debug.Log("Audio Controller : " + AudioController.Instance);

            AudioController.Instance.PlayAudio(PositiveMenuInput);

            Fader.Instance.FadeOut(delegate 
            {
                SceneManager.LoadScene("Game");
                UIController.Instance.MenuContainer.SetActive(false);
                UIController.Instance.UIContainer.SetActive(true);
                GameController.Instance.GoToGameState(GameController.eGameState.PreGame);
                Fader.Instance.FadeIn();
            });
        }
    }
}