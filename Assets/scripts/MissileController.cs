using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    GameObject target;
    bool isFired = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void fire()
    {
        isFired = true;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.forward * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);

        // TODO: make sure target exists and follow target
        if (isFired)
        {
            transform.position += transform.forward.normalized * 0.05f;
        }
    }
}
