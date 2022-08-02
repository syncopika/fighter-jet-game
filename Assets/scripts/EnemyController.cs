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
            Vector3 fromTarget = (target.transform.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(fromTarget);

            // TODO: try rotating about z axis if turning left or right to produce a more realistic visual
            // helpful? https://answers.unity.com/questions/315723/flight-movement-with-banking.html
            float bankAmount = 30f * Vector3.Dot(transform.up, fromTarget); // use up here b/c up is actually the vector along the x-axis
            
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 15f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 15f * Time.deltaTime);

            transform.rotation *= Quaternion.AngleAxis(bankAmount, Vector3.forward); // rotate about the forward axis

            transform.position += transform.forward * 12f * Time.deltaTime;
        }

        isTargeted = false;

        Vector3 fwd = transform.forward * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);
        Debug.DrawRay(transform.position, transform.up * 10, Color.green);
        Debug.DrawRay(transform.position, transform.right * 10, Color.blue);
    }
}
