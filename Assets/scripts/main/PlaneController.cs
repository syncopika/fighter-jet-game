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
    bool isFlying = true;
    bool isDead = false;

    Transform target;

    // armament
    public GameObject missile1;
    public GameObject missile2;
    public GameObject bomb1;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRend = GetComponent<SkinnedMeshRenderer>();
    }

    void explode()
    {
        // TODO - explosion effects? display game over message?
        //Debug.Log("im dead");
        isDead = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Equals("ground") || 
            collision.transform.name.Contains("Cube") ||
            collision.transform.name.Contains("f5"))
        {
            explode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // https://www.grc.nasa.gov/www/k-12/airplane/airplane.html

        if (isDead)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            // move forward
            transform.position += transform.forward * speed * Time.deltaTime;
            isFlying = true;
        }

        if (Input.GetKey(KeyCode.E))
        {
            // rotate right about the Z axis
            transform.Rotate(new Vector3(0, 0, -1) * 75 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            // rotate left about the Z axis
            transform.Rotate(new Vector3(0, 0, 1) * 75 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // move left
            transform.Rotate(Vector3.left * 25 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // move right
            transform.Rotate(Vector3.right * 25 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // move up
            transform.Rotate(Vector3.up * 25 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // move down
            transform.Rotate(-Vector3.up * 25 * Time.deltaTime);
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

        // do a spherecast to detect enemies ahead
        // https://docs.unity3d.com/ScriptReference/Physics.SphereCast.html
        RaycastHit hit;
        float radius = 6f;
        Vector3 pos = transform.position;

        if(Physics.SphereCast(pos, radius, transform.forward, out hit, 120f))
        {
            if (hit.transform.name.Contains("f5tiger"))
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

                if (targetAcquired)
                {
                    // set missile to target
                    missile2.GetComponent<MissileController>().setTarget(target);
                }

                missile2 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bomb1)
            {
                bomb1.GetComponent<BombController>().fire();
                bomb1 = null;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || !isFlying)
        {
            // plane has stopped moving so it should fall
            isFlying = false;
            transform.position -= Vector3.up * 20f * Time.deltaTime;
            transform.Rotate(new Vector3(-0.1f, Random.Range(0.1f, 0.15f), 0.1f));
        }

        target = null;
        targetAcquired = false;
        Vector3 fwd = transform.forward * 10;
        Debug.DrawRay(transform.position, fwd, Color.green);
    }
}
