using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject target; // the target of the enemy, e.g. the player
    public Material isTargetedMaterial; // material to show when this model is in attack range of the player

    bool isDead = false;
    bool isTargeted = false;

    Material originalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        explosion.GetComponent<ParticleSystem>().Pause();
        originalMaterial = transform.GetComponent<MeshRenderer>().material;
    }

    public void die()
    {
        // show explosion
        explosion.GetComponent<ParticleSystem>().Play();
        isDead = true; // TODO: delete model from scene
    }

    public void targeted()
    {
        isTargeted = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!isDead)
        {
            transform.position += transform.forward * 12f * Time.deltaTime;
        }*/

        if (isTargeted)
        {
            transform.GetComponent<MeshRenderer>().material = isTargetedMaterial;
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material = originalMaterial;
        }

        if (target && !isDead)
        {
            // logic to move to attack target

            // find where the target is

            /* possible conditions to consider? (if the action may be different in each):
               - if target is above
               - if target is below
               - if target is at about the same altitude
            */

            Vector3 fromTarget = (target.transform.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(fromTarget);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 15f * Time.deltaTime);
            transform.position += transform.forward * 12f * Time.deltaTime;
        }

        isTargeted = false;
    }
}
