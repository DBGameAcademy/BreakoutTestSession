using System.Collections;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    Ball ball;
    Paddle paddle;

    public enum eGameState
    {
        MainMenu,
        PreGame,
        Play,
        Paused,
        PostGame
    }
    public eGameState GameState = eGameState.MainMenu;

    private void Start()
    {
        Fader.Instance.FadeIn();
    }

    public void GoToGameState(eGameState _gameState)
    {
        GameState = _gameState;

        // enter new state.
        switch (GameState)
        {
            case eGameState.MainMenu:
                    
                break;

            case eGameState.PreGame:

                UIController.Instance.MenuContainer.SetActive(false);
                UIController.Instance.UIContainer.SetActive(true);

                UIController.Instance.CountdownText.BeginCountdown(3);

                PlayerController.Instance.SetupGame(); 

                break;

            case eGameState.Play:

                break;

            case eGameState.PostGame:

                break;
        }
    }
}