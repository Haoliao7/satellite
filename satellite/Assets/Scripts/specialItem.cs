using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialItem : MonoBehaviour
{
    int index; // index to decide which item you are
    GameObject player;
    GameObject duplicate;
    Rigidbody rb;
    public float speed;

    public GameObject shipBody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        duplicate = GameObject.Find("Duplicate");
        index = Random.Range(1, 5);

        rb = gameObject.GetComponent<Rigidbody>();

        if(index == 1)
        {
            shipBody.GetComponent<Renderer>().material.color = Color.blue;
        }
        if(index == 2)
        {
            shipBody.GetComponent<Renderer>().material.color = Color.red;
        }
        if(index == 3)
        {
            shipBody.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (index == 4)
        {
            shipBody.GetComponent<Renderer>().material.color = Color.green;
        }

        StartMoving();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartMoving()
    {
        if (transform.position.x >= 11)
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }

        if (transform.position.x <= -11)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }

        if (transform.position.y >= 7)
        {
            rb.velocity = new Vector3(0, -speed, 0);
        }

        if (transform.position.y <= -7)
        {
            rb.velocity = new Vector3(0, speed, 0);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (index == 1)
            {
               /* player.GetComponent<PlayerMovement>().Enlarge();
                if (duplicate != null)
                {
                    duplicate.GetComponent<PlayerMovement>().Enlarge();
                }*/

                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.enlarge);

            }
            if (index == 2)
            {
                /* player.GetComponent<PlayerMovement>().SpeedUp();
                 if (duplicate != null)
                 {
                     duplicate.GetComponent<PlayerMovement>().SpeedUp();
                 }*/

                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.speedUp);
            }
            if (index == 3)
            {
                //player.GetComponent<PlayerMovement>().SetActivateDuplicate();

                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.duplicate);

            }
            if(index == 4)
            {
                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.shield);
            }

            Destroy(gameObject);
        }
    }

    

}
