using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private const float Speed = 10F;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0F, Speed);
    }

    private void Update()
    {
        if (transform.position.y >= 9F)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
