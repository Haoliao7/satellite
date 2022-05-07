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
        rb = GetComponent<Rigidbody>(); //access its rigidbody
        Vector3 dir = earthPos - transform.position; //get the direction from earth to its position
        dir.Normalize(); // normalize it so all the bullet speed will be the same
        

        transform.LookAt(earthPos); // rotate the bullet so it will point to the earth
        transform.Rotate(90, 0, 0);

        rb.velocity = dir * speed; //set a velocity so it will move toward to the earth
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject); //if it collides with the player, destroy it
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.name == "Shield")
        {
            Destroy(gameObject); // it it collides with the shield, destroy it
        }

    }

}
