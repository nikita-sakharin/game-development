using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public SpriteRenderer renderer;

    private static List<Rigidbody2D> circles = new List<Rigidbody2D>();
    private float radius;

    void Start()
    {
        Vector2 radiusVector = rigidbody.GetComponent<Transform>().localScale;
        radius = radiusVector.magnitude * Mathf.Sqrt(2F);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var res = (from circle in circles
                where (circle.position - mousePosition).magnitude < .5F
                select circle).FirstOrDefault();

            if (res != null)
                res.GetComponent<SpriteRenderer>().color = Color.red;
            else
            {
                renderer.color = Random.ColorHSV();
                circles.Add(Instantiate(rigidbody,
                    new Vector3(mousePosition.x, mousePosition.y, 0F),
                    Quaternion.identity));
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            var redCircles = (from circle in circles
                where circle.GetComponent<SpriteRenderer>().color == Color.red
                select circle).ToList();
            redCircles.ForEach(circle =>
            {
                circle.AddForce((mousePosition - circle.transform.position));
                if ((circle.transform.position - mousePosition).magnitude <= radius)
                {
                    circle.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
                }
            });
        }
    }
}
