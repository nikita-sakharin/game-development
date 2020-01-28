using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Quaternion initialRotation;
    private Vector2 lastMousePosition;
    private int xRotation, yRotation;

    public float speed = 2F;
    public GameObject[] cubeArray = new GameObject[6];

    private void Start()
    {
        initialRotation = transform.rotation;
        xRotation = yRotation = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Input.GetKey("space"))
        {
            float dx = Input.mousePosition.x - lastMousePosition.x,
                  dy = Input.mousePosition.y - lastMousePosition.y;
            xRotation -= (int) Mathf.Round(dx);
            xRotation %= 360;

            yRotation += (int) Mathf.Round(dy);
            yRotation %= 360;

            Quaternion xQuat = Quaternion.AngleAxis(xRotation, Vector3.up);
            Quaternion yQuat = Quaternion.AngleAxis(yRotation, Vector3.right);
            transform.rotation = initialRotation * yQuat * xQuat;
        }

        lastMousePosition = Input.mousePosition;

        HandleKey(KeyCode.RightArrow, Vector3.up, -speed);
        HandleKey(KeyCode.LeftArrow, Vector3.up, speed);

        HandleKey(KeyCode.UpArrow, Vector3.right, speed);
        HandleKey(KeyCode.DownArrow, Vector3.right, -speed);
    }

    private void HandleKey(KeyCode key, Vector3 axis, float angle)
    {
        if (Input.GetKey(key))
            foreach (GameObject cube in cubeArray)
                cube.transform.RotateAround(Vector3.zero, axis, angle);
    }
}
