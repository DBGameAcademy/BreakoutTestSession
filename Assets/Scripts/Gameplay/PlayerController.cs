using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    float moveSpeed = 10f;

    int currentLives = 3;
    const int INITIAL_LIVES = 3;
    const int MAX_LIVES = 5;

    float gunTimer = 0f;
    const float GUN_DURATION = 30.0f;
    float lastFireTime;
    const float FIRE_RATE = 0.2f;

    Paddle playerPaddle;
    Ball ball;

    public int Score = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GameState != GameController.eGameState.Play)
        {
            return;
        }

        if (InputController.Instance.GetInput(InputController.InputAction.Left))
        {
            playerPaddle.xVelocity -= moveSpeed * Time.deltaTime;
            //playerPaddle.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (InputController.Instance.GetInput(InputController.InputAction.Right))
        {
            playerPaddle.xVelocity += moveSpeed * Time.deltaTime;
            //playerPaddle.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (gunTimer > 0 && InputController.Instance.GetInput(InputController.InputAction.Fire))
        {
            if (lastFireTime + FIRE_RATE > Time.time)
            {
                //spawn a bullet at the paddle.
                PoolObject bullet = PoolManager.Instance.GetObjectFromPool(PoolObjectItem.ePoolItem.Bullet);
                bullet.transform.position = playerPaddle.transform.position;
                lastFireTime = Time.time;
            }
        }
        else
        {
            gunTimer -= Time.deltaTime;
        }
    }

    public void CollectPowerUp(PowerUp.ePowerUpType _type)
    {
        switch (_type)
        {
            case PowerUp.ePowerUpType.ExtraLife:

                currentLives++;
                if (currentLives > MAX_LIVES)
                    currentLives = MAX_LIVES;

                //update the UI
                UIController.Instance.PlayerLives.UpdateLives(currentLives);

                break;

            case PowerUp.ePowerUpType.Wide:
                playerPaddle.transform.localScale = new Vector3(2, 1, 1);
                break;

            case PowerUp.ePowerUpType.Thin:
                playerPaddle.transform.localScale = new Vector3(0.5f, 1, 1);
                break;

            case PowerUp.ePowerUpType.Gun:
                gunTimer = GUN_DURATION;
                break;
        }
    }

    public void SetupGame()
    {
        SpawnPaddle();
        SpawnBall();
        currentLives = INITIAL_LIVES;
        UIController.Instance.PlayerLives.UpdateLives(currentLives);
        Score = 0;
        UIController.Instance.UpdateScore(Score);
    }

    void SpawnPaddle()
    {
        PoolObject paddle = PoolManager.Instance.GetObjectFromPool(PoolObjectItem.ePoolItem.Paddle);
        paddle.ActivatePoolObject();
        paddle.transform.position = new Vector3(0, -3, 0);
        playerPaddle = paddle.GetComponent<Paddle>();
    }

    void SpawnBall()
    {
        PoolObject ballPO = PoolManager.Instance.GetObjectFromPool(PoolObjectItem.ePoolItem.Ball);
        ballPO.ActivatePoolObject();
        ball = ballPO.GetComponent<Ball>();
    }

    public void BallLost()
    {
        currentLives--;
        UIController.Instance.PlayerLives.UpdateLives(currentLives);
        if (currentLives < 0)
        {
            //Game Over!
            GameController.Instance.GoToGameState(GameController.eGameState.PostGame);
        }
        else
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        //make sure we go up not down
        ball.Velocity.y = Mathf.Abs(ball.Velocity.y);
    }

    public void UpdateScore(int _value)
    {
        Score += _value;
        UIController.Instance.UpdateScore(Score);
    }
}