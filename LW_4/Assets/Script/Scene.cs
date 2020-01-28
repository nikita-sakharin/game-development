using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scene : MonoBehaviour
{
    public const float Frequency = 0.05F;
    public Rigidbody2D asteroid;

    private void FixedUpdate()
    {
        if(Random.value < Frequency)
            NewAsteroid();
    }

    private void NewAsteroid()
    {
        Vector2 position = new Vector2(Random.Range(-5F, 5F), 9F);
        Rigidbody2D rigidbody = Instantiate(asteroid, position,
            Quaternion.identity);
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
