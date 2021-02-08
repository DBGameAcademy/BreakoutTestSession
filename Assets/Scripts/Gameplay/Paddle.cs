using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float MoveSpeed;

    public GameObject ball;
    public bool AutoPlay;

    public float xVelocity = 0;
    float deceleration = 0.2f;

    private void Update()
    {
        if (GameController.Instance.GameState == GameController.eGameState.Play)
        {
            if (AutoPlay)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(ball.transform.position.x, transform.position.y, 0), Time.deltaTime * MoveSpeed);
            }
            else
            {
                transform.Translate(new Vector3(xVelocity, 0, 0));
                xVelocity *= deceleration;
            }
        }
       
    }

    public void GoWide()
    {
        transform.localScale = new Vector3(1.5f, 1, 1);
    }
}
