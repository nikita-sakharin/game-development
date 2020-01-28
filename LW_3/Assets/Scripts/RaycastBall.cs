using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastBall : MonoBehaviour
{
    private static List<Rigidbody> ballList = new List<Rigidbody>();
    private float radius;

    public Rigidbody ballRigidbody;
    public float accelerate = 10F, width = 0.03125F;

    void Start()
    {
        radius = ballRigidbody.GetComponent<Transform>().localScale.magnitude / 2F;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitArray = Physics.RaycastAll(ray);
        if (Input.GetMouseButtonDown(0) && !Input.GetKey("space"))
        {
            Debug.Log("Input.GetMouseButtonDown(0) && !Input.GetKey(\"space\")");
            Renderer hitBallRenderer = (from hit in hitArray
                where "Ball".Equals(hit.transform.tag)
                select hit.transform.GetComponent<Renderer>()).FirstOrDefault();
            if (hitBallRenderer != null)
            {
                hitBallRenderer.material.color = Color.red;
                return;
            }

            Vector3 average = AverageCubeHit(hitArray);
            if (average[0] != average[0])
                return;

            Debug.Log("new");
            Rigidbody newBall = Instantiate(ballRigidbody, average,
                Quaternion.identity);
            newBall.GetComponent<Renderer>().material.color = Random.ColorHSV();
            newBall.useGravity = true;

            ballList.Add(newBall);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            List<Rigidbody> redBall = (from ball in ballList
                where ball.GetComponent<Renderer>().material.color == Color.red
                select ball).ToList();

            Vector3 point = AverageCubeHit(hitArray);
            if (point[0] != point[0])
                return;
            foreach (Rigidbody ball in redBall)
            {
                Vector3 delta = point - ball.transform.position;
                Debug.Log(delta);
                ball.AddForce(delta * accelerate);
                if (delta.magnitude < radius)
                    ball.GetComponent<Renderer>().material.color = Random.ColorHSV();
            };
        }
    }

    private Vector3 AverageCubeHit(RaycastHit[] hitArray)
    {
        List<Vector3> hitCube = (from hit in hitArray
            where "Cube".Equals(hit.transform.tag)
            select hit.point).ToList();
        int count = hitCube.Count;

        Vector3 average = new Vector3(0F, 0F, 0F);
        foreach (Vector3 hit in hitCube)
            average += hit;
        average /= count;

        for (int i = 0; i < 3; ++i)
            if (!(Mathf.Abs(average[i]) < 1F - width / 2F))
                return average * 0F / 0F;

        return average;
    }
}
