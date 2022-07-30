using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    Transform target;

    public GameObject smoke;
    private bool isFired = false;

    // Start is called before the first frame update
    void Start()
    {
        smoke.GetComponent<ParticleSystem>().Pause();
    }
    
    public void fire()
    {
        isFired = true;
        transform.parent = null;
        smoke.GetComponent<ParticleSystem>().Play();
    }

    public void setTarget(Transform tgt)
    {
        target = tgt;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.forward * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);

        // TODO: make sure target exists and follow target
        if (isFired)
        {
            // TODO: follow target
            transform.position += transform.forward.normalized * 0.05f;
        }
    }
}
