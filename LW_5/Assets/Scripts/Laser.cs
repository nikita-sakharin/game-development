using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField]
    private GameObject explosion;
    private CobraInterceptor cobra;

    public CobraInterceptor Cobra {
        set { cobra = value; }
    }

    private void Start() {
        const float LaserDelta = 34, SQRT1_2 = 7.0710678E-1F, Velocity = 2000;
        transform.rotation = new Quaternion(SQRT1_2, 0, 0, SQRT1_2);
        transform.Rotate(new Vector3(0, -Mathf.Rad2Deg * cobra.Angle));
        Vector3 normal = new Vector3(-cobra.transform.position.y,
            cobra.transform.position.x).normalized;
        GetComponent<Rigidbody>().velocity = normal * Velocity;
        transform.position = cobra.transform.position + normal * (LaserDelta +
            Scene.R * Time.fixedDeltaTime * cobra.Velocity);
        transform.localScale = new Vector3(10, 10, 10);
    }

    private void OnCollisionEnter(Collision other) {
        if (!"Torus".Equals(other.gameObject.tag)) {
            Destroy(Instantiate(explosion, transform.position,
                transform.rotation), 10);
            Destroy(other.gameObject);
            cobra.Score += 100;
        }
        Destroy(gameObject);
    }
}
