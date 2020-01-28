using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    private const float Down = -4.5F, Left = -5F, Right = -Left, Up = -Down,
        LaserDelta = .515625F, SpeedX = .05F, SpeedY = .05F;

    private Quaternion startRotation;
    private TimeSpan reload = new TimeSpan(0, 0, 0, 0, 500),
        shootTime = DateTime.Now.TimeOfDay;
    private float barStartExtents, barStartPosition, barStartScale, health = 1F;

    public GameObject healthBar;
    public Rigidbody2D laser;

    void Start()
    {
        barStartExtents = healthBar.GetComponent<Renderer>().bounds.extents.x;
        barStartPosition = healthBar.transform.position.x;
        barStartScale = healthBar.transform.localScale.x;
        startRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = startRotation;
        Vector2 position = transform.position;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation *= Quaternion.AngleAxis(30F, Vector3.forward);
            position.x = Mathf.Max(Left, position.x - SpeedX);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation *= Quaternion.AngleAxis(-30F, Vector3.forward);
            position.x = Mathf.Min(Right, position.x + SpeedX);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position.y = Mathf.Min(Up, position.y + SpeedY);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y = Mathf.Max(Down, position.y - SpeedY);
        }
        transform.position = position;

        if (Input.GetKey("space"))
        {
            ShootLaser();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!"Asteroid".Equals(collision.gameObject.tag))
            return;

        Vector2 collisionScale = collision.gameObject.transform.localScale;
        health -= (collisionScale.x + collisionScale.y) / 2F;

        if (health < 0F)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Destroy(collision.gameObject);
        float x = barStartPosition - barStartExtents * (1F - health);
        healthBar.transform.position = new Vector2(x,
            healthBar.transform.position.y);
        healthBar.transform.localScale = new Vector2(barStartScale * health,
            healthBar.transform.localScale.y);
    }

    private void ShootLaser()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;
        if (now.CompareTo(shootTime.Add(reload)) < 0)
            return;
        shootTime = now;

        Vector2 position = new Vector2(transform.position.x,
            transform.position.y + LaserDelta);
        Rigidbody2D rigidbody = Instantiate(laser, position,
            Quaternion.identity);
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX |
            RigidbodyConstraints2D.FreezeRotation;
    }
}
