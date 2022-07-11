using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CobraInterceptor : MonoBehaviour {
    private readonly TimeSpan reload = new TimeSpan(0, 0, 0, 0, 125);
    private readonly Vector2 scale = new Vector2(5, 5);

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private Laser laser;
    [SerializeField]
    private Scene scene;
    [SerializeField]
    private Text levelText, livesText, scoreText;

    private Quaternion startRotation = Quaternion.identity;
    private TimeSpan shootTime = DateTime.Now.TimeOfDay;
    private float angle = 0, deltaR = 0, velocity = 0, score = 0;
    private int lives = 3;

    public float Angle {
        get { return angle; }
    }
    public float Velocity {
        get { return velocity; }
    }
    public float Score {
        get { return score; }
        set { score = value; }
    }

    private void Start() {
        startRotation = transform.rotation;
        UpdateVelocity();
    }

    private void FixedUpdate() {
        transform.rotation = startRotation;
        transform.Rotate(new Vector3(0, -Mathf.Rad2Deg * angle));

        Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) *
            (Scene.R + deltaR);
        position.z = transform.position.z;
        transform.position = position;

        angle = (angle + velocity * Time.fixedDeltaTime) % (2 * Mathf.PI);

        UpdateDeltaRAndZ();
        UpdateVelocity();

        if (Input.GetKey("space"))
            ShootLaser();
        score += Time.fixedDeltaTime;
        UpdateLevelAndScore();
    }

    private void OnTriggerEnter(Collider other) {
        if ("Laser".Equals(other.gameObject.tag))
            return;
        DecrementLives();
        Destroy(Instantiate(explosion, transform.position,
            transform.rotation), 10);
        Destroy(other.gameObject);
        Time.timeScale = 0.125F;
        Invoke("Reset", 0.5F);
    }

    private void Reset() {
        Time.timeScale = 1;
        if (lives > 0)
            return;
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetFloat("score", score);
        SceneManager.LoadScene("InputRecord");
    }

    private void UpdateVelocity() {
        const float Lower = 0.25F, Upper = 0.5F, Delta = 9.765625E-4F;
        if (Input.GetKey(KeyCode.KeypadPlus))
            velocity += Delta;
        if (Input.GetKey(KeyCode.KeypadMinus))
            velocity -= Delta;
        velocity = Mathf.Min(Mathf.Max(velocity, Lower), Upper);
    }

    private void UpdateDeltaRAndZ() {
        const float Lower = -350, Upper = 350;

        float horizontal = Input.GetAxis("Horizontal"),
            vertical = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(-30 * vertical, 0, -30 * horizontal));
        deltaR += horizontal * scale.x;
        deltaR = Mathf.Min(Mathf.Max(deltaR, Lower), Upper);
        float z = transform.position.z - vertical * scale.y;
        z = Mathf.Min(Mathf.Max(z, Lower), Upper);
        transform.position =
            new Vector3(transform.position.x, transform.position.y, z);
    }

    private void ShootLaser() {
        TimeSpan now = DateTime.Now.TimeOfDay;
        if (now.CompareTo(shootTime.Add(reload)) < 0)
            return;
        shootTime = now;

        Laser newLaser = Instantiate(laser, Vector3.zero, Quaternion.identity);
        newLaser.Cobra = this;
    }

    private void DecrementLives() {
        livesText.text = "Lives: " + --lives;
    }

    private void UpdateLevelAndScore() {
        if (score >= 5000 && scene.Level < 2) {
            scene.Level = 2;
        } else if (score >= 2000 && scene.Level < 1) {
            scene.Level = 1;
        }

        scoreText.text = "Score: " + Mathf.Round(score);
        levelText.text = "Level: " + (scene.Level + 1);
    }
}
