using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject bombObject;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion.GetComponent<ParticleSystem>().Pause();
        ParticleSystem.MainModule main = explosion.GetComponent<ParticleSystem>().main;
        main.loop = false;
    }

    public void fire()
    {
        Rigidbody rb = bombObject.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 70;

        bombObject.AddComponent<BoxCollider>();

        transform.parent = null;
    }

    public void setTarget(Transform tgt)
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Equals("ground") || collision.transform.name.Contains("Cube"))
        {
            explode();
        }
    }

    void explode()
    {
        // explosion
        explosion.GetComponent<ParticleSystem>().Play();

        float radius = 15f;
        float force = 800f;
        float upwardsModifier = 6f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();

            if (!c.transform.name.Contains("f14") && rb) rb.AddExplosionForce(force, transform.position, radius, upwardsModifier);
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = -transform.up * 20 * Time.deltaTime;
        Debug.DrawRay(transform.position, fwd, Color.green);
    }
}
