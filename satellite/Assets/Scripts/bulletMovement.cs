using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public Vector3 earthPos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = earthPos - transform.position;
        dir.Normalize();
        

        transform.LookAt(earthPos);
        transform.Rotate(90, 0, 0);

        rb.velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.name == "Shield")
        {
            Destroy(gameObject);
        }

    }

}
