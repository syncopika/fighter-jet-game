using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject bombObject;
    public ParticleSystem explosion;

    //Transform target;

    private bool isDead = false;
    private bool isFired = false;

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
        rb.mass = 100;

        isFired = true;
        transform.parent = null;
    }

    public void setTarget(Transform tgt)
    {
    }

    void checkCollision()
    {
        RaycastHit hit;

        // TODO: if plane is inverted or rotated, transform.up is flipped! need to handle that properly
        if (isFired && Physics.Raycast(bombObject.transform.position, -transform.up, out hit, 1f))
        {
            // explosion
            explosion.GetComponent<ParticleSystem>().Play();

            //target = null;
            isDead = true; // TODO: just delete model from scene

            Destroy(bombObject.GetComponent<Rigidbody>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead) checkCollision();

        Vector3 fwd = -transform.up * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);
    }
}
