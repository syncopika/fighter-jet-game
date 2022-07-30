using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    Transform target;

    public GameObject smoke;

    private bool isDead = false;
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

    void checkCollision()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            hit.transform.GetComponent<EnemyController>().die();
            target = null;
            isDead = true; // TODO: just delete model from scene
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.forward * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);

        if (isFired && !isDead)
        {
            transform.position += transform.forward.normalized * 0.05f;

            // modify rotation so it constantly faces target
            if (target)
            {
                // https://answers.unity.com/questions/1663326/smoothly-rotate-object-towards-target-object.html
                Vector3 fromTarget = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(fromTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 3f * Time.deltaTime);

                checkCollision();
            }
        }
    }
}
