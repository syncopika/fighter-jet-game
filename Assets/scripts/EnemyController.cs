using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosion;
    public Material targetedMaterial;

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
        if (!isDead)
        {
            transform.position += transform.forward * 12f * Time.deltaTime;
        }

        if (isTargeted)
        {
            transform.GetComponent<MeshRenderer>().material = targetedMaterial;
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material = originalMaterial;
        }

        isTargeted = false;
    }
}
