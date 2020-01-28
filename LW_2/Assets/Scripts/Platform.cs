using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D platform;
    public float speed = 3F;

    public void Start()
    {
        platform = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Vector2 velocity = new Vector2(0F, platform.velocity.y);
        if (Input.GetAxis("Horizontal") > 0)
            velocity.x = speed;
        else if (Input.GetAxis("Horizontal") < 0)
            velocity.x = -speed;
        platform.velocity = velocity;
    }
}
