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
        player = GameObject.Find("Player"); //access the player gameobject
        duplicate = GameObject.Find("Duplicate"); // access the clone moon gameobject
        index = Random.Range(1, 5); // get a random number between 1~4

        rb = gameObject.GetComponent<Rigidbody>();//access its rigidbody

        if(index == 1)//give it a color according the number
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
        if (transform.position.x >= 11) // it it's on the right side
        {
            rb.velocity = new Vector3(-speed, 0, 0); //go left
        }

        if (transform.position.x <= -11)// it it's on the left side
        {
            rb.velocity = new Vector3(speed, 0, 0);//go right
        }

        if (transform.position.y >= 7)// it it's above the screen
        {
            rb.velocity = new Vector3(0, -speed, 0);//go down
        }

        if (transform.position.y <= -7)//if it's below the screen
        {
            rb.velocity = new Vector3(0, speed, 0);//go up
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (index == 1) //if it's No.1 special item
            {
               
                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.enlarge); //change the player's state

            }
            if (index == 2)//if it's No.2 special item
            {
                
                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.speedUp);//change the player's state
            }
            if (index == 3)//if it's No.3 special item
            {

                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.duplicate);//change the player's state

            }
            if(index == 4)//if it's No.4 special item
            {
                player.GetComponent<PlayerMovement>().stateTransition(PlayerMovement.State.shield);//change the player's state
            }

            Destroy(gameObject);//destroy this item
        }
    }

    

}
