using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject lastHitGameObject = null;

    CircleCollider2D circleCollider;
    Rigidbody2D rb;

    public AudioObject WallImpactAudio;
    public AudioObject PaddleImpactAudio;

    public Vector2 Velocity = new Vector2(4,4);

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GameState == GameController.eGameState.Play)
        {
            UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, circleCollider.radius, Velocity, (Velocity * Time.deltaTime).magnitude);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != circleCollider && hit.transform.gameObject != lastHitGameObject)
            {
                 Velocity = Vector2.Reflect(Velocity, hit.normal);

                lastHitGameObject = hit.transform.gameObject;

                //did we hit a destroyable?
                if (hit.transform.GetComponent<Block>())
                {
                    hit.transform.GetComponent<Block>().OnDestroy();
                }

                //did we hit a paddle?
                else if (hit.transform.GetComponent<Paddle>())
                {
                    AudioController.Instance.PlayAudio(PaddleImpactAudio);

                    //  //correct the velocity to always go upwards as a grace to the player (hitting the side, hitting multiple times etc)
                    Velocity.y = Mathf.Abs(Velocity.y);

                    //clamp y velocity to a minimum to stop shallow bouncing around the map
                    Velocity.y = Mathf.Clamp(Velocity.y, 0.75f, 10f);

                    //add the paddle velocity to the ball to make it *pushed* on impact
                     Velocity.x += hit.transform.GetComponent<Paddle>().xVelocity;
                }
                else
                {
                    // play default impact for hitting walls
                    AudioController.Instance.PlayAudio(WallImpactAudio);
                }

                break;
            }
        }

        //simple check for leaving bottom of screen
        if (transform.position.y < -4)
        {
            PlayerController.Instance.BallLost();
        }
    }
}
