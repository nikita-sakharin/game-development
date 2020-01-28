using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private const int Lives = 3, numOfBlocks = 24;

    public GameObject platform, text;
    public GameObject[] Live = new GameObject[3];
    public float acceleration = 1F;

    private Rigidbody2D ball;
    private int lives = Lives, destroyed = 0;

    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        Time.timeScale = 0F;
        textLivesRemain();
        reset();
    }

    void Update()
    {
        if (destroyed >= numOfBlocks)
        {
            Time.timeScale = 0F;
            text.GetComponent<Text>().text = "You win!\nPress 'space' for new game";
        }

        if (transform.position.y <= -5F)
        {
            Time.timeScale = 0F;
            decrementLives();
            if (lives <= 0)
                text.GetComponent<Text>().text = "Game over.\nPress 'space' for restart.";
            else
                textLivesRemain();
            reset();
        }

        if (lives <= 0 || destroyed >= numOfBlocks)
        {
            if (Input.GetKeyDown("space"))
            {
                Time.timeScale = 1F;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (Time.timeScale == 0F && Input.GetKeyDown("space"))
        {
            text.GetComponent<Text>().text = "";
            Time.timeScale = 1F;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Block":
                Destroy(collision.gameObject);
                ++destroyed;
                ball.velocity = new Vector2(
                    ball.velocity.x * 1.03125F,
                    ball.velocity.y * 1.03125F);
                break;

            case "Platform":
                float diff = (platform.transform.position.x - ball.transform.position.x);
                Vector2 vel = new Vector2(ball.velocity.x - 2F * diff, -ball.velocity.y);
                vel.Normalize();
                vel.Scale(new Vector2(4F, 4F));
                ball.velocity = vel;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    }

    private void reset()
    {
        transform.position = new Vector2(0F, -3F);
        platform.transform.position = new Vector3(0F, -5F);
        ball.velocity = new Vector2(0F, -3F);
        float deltaTime = (Time.deltaTime > 0F ? Time.deltaTime : 1F / 60F),
            x = UnityEngine.Random.Range(-0.5F, 0.5F);
        ball.AddForce(new Vector2(x, -1.125F) * Time.deltaTime * acceleration);
    }

    private void textLivesRemain()
    {
        text.GetComponent<Text>().text = "Lives remain: " + lives + ".\nPress 'space' to continue.";
    }

    private void decrementLives()
    {
        Live[--lives].SetActive(false);
    }
}
