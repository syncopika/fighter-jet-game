using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRend;

    float speed = 15f;
    float blendOneVal = 0f;

    bool sweptWing = false; // this is specific only to the F-14
    bool targetAcquired = false;
    Transform target;

    // armament
    public GameObject missile1;
    public GameObject missile2;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRend = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // https://www.grc.nasa.gov/www/k-12/airplane/airplane.html

        if (Input.GetKey(KeyCode.W))
        {
            // move forward
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            // rotate right about the Z axis
            transform.Rotate(new Vector3(0, 0, -1) * 60 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            // rotate left about the Z axis
            transform.Rotate(new Vector3(0, 0, 1) * 60 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // move left
            transform.Rotate(Vector3.left * 20 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // move right
            transform.Rotate(Vector3.right * 20 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // move up
            transform.Rotate(Vector3.up * 20 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // move down
            transform.Rotate(-Vector3.up * 20 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if(sweptWing)
            {
                // go down to 0 -> open wing config
                blendOneVal -= 0.1f;
                if(blendOneVal <= 0)
                {
                    blendOneVal = 0;
                    sweptWing = false;
                }
            }
            else
            {
                // swept-wing config
                blendOneVal += 0.1f;

                if(blendOneVal >= 100)
                {
                    blendOneVal = 100;
                    sweptWing = true;
                }
            }
            skinnedMeshRend.SetBlendShapeWeight(0, blendOneVal);
        }

        // do a spherecast to detect enemies
        // https://docs.unity3d.com/ScriptReference/Physics.SphereCast.html
        RaycastHit hit;
        float radius = 3f;
        Vector3 pos = transform.position;

        if(Physics.SphereCast(pos, radius, transform.forward, out hit, 90))
        {
            if (hit.transform.name.Equals("f5tiger"))
            {
                hit.transform.GetComponent<EnemyController>().targeted();
                targetAcquired = true;
                target = hit.transform;
            }
        }

        // use keydown to avoid capturing the key more than once with one press
        if (Input.GetKeyDown(KeyCode.F))
        {
            // fire missile
            if (missile1) {
                missile1.GetComponent<MissileController>().fire();

                if (targetAcquired)
                {
                    // set missile to target
                    missile1.GetComponent<MissileController>().setTarget(target);
                }

                missile1 = null;

            } else if (missile2)
            {
                missile2.GetComponent<MissileController>().fire();
                missile2 = null;
            }
        }

        target = null;
        targetAcquired = false;
        Vector3 fwd = transform.forward * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);
    }
}
