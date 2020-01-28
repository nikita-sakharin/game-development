using UnityEngine;

public class Scene : MonoBehaviour {
    public const float R = 1000, TubeR = 422;

    [SerializeField]
    private Rigidbody[] asteroidArray;
    [SerializeField]
    private CobraInterceptor cobra;

    private readonly float[] asteroidFrequency = new float[3] { 1, 2, 5 },
        velocity = new float[3] { 5, 10, 20 }, t = new float[3] { 30, 60, 90 };

    private int level = 0;

    public int Level {
        get { return level; }
        set { level = value; }
    }

    private void Start() {
        level = PlayerPrefs.GetInt("level");
    }

    private void FixedUpdate() {
        if(Random.value / Time.fixedDeltaTime < asteroidFrequency[level])
            NewAsteroid();
    }

    private void NewAsteroid() {
        int i = Random.Range(0, asteroidArray.Length);
        Rigidbody asteroid = Instantiate(asteroidArray[i],
            RandomPosition(-Random.Range(15F, 180F)), Quaternion.identity);
        asteroid.velocity = Random.onUnitSphere * velocity[level];
        asteroid.transform.localScale = new Vector3(Random.Range(25F, 50F),
            Random.Range(25F, 50F), Random.Range(25F, 50F));
        Destroy(asteroid.gameObject, t[level]);
    }

    private Vector3 RandomPosition(float deltaAngleDeg) {
        float angle = cobra.Angle + Mathf.Deg2Rad * deltaAngleDeg;
        Vector2 insideCircle = Random.insideUnitCircle * TubeR;
        Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) *
            (R + insideCircle.x);
        position.z = insideCircle.y;
        return position;
    }
}
