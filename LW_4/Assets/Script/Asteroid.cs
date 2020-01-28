using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private const float Speed = 2F, Rotation = 10F;

    private void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0F, -Speed * Random.Range(.5F, 2F));

        float random = Random.Range(-.5F, .5F);
        random += Mathf.Sign(random) * .5F;
        rigidbody.angularVelocity = Rotation * random;

        Vector2 scale = transform.localScale;
        scale.Scale(new Vector2(Random.Range(.5F, 1.25F), Random.Range(.5F, 1.25F)));
        transform.localScale = scale;
    }

    private void Update()
    {
        if (transform.position.y <= -9F)
            Destroy(gameObject);
    }
}
