using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public GameObject manager;
    void captured()
    {
        // this sphere has been captured so make it dissapear and tell the gamemanager
        manager.transform.GetComponent<MountainGameManager>().sphereCollected();

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        captured();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * 30 * Time.deltaTime);
    }
}
