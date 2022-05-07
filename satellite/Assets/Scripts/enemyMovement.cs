using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    //public GameObject warningSquare;
    public float timeBeforeMoving;

    float scaling;
    public float scalingSpeed;
    public Vector3 earthPos;

    public GameObject shipBody;

    int index;

    [Header("shooting")]
    Vector3 target;
    float timer;
    public float timeGap;
    public GameObject bulletPrefab;

    [Header("bomb")]
    GameObject player;
    bool onTrack;
    float randomPos;
    public float countDown;
    public GameObject countDownText;
    public GameObject explosionPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();//access its rigidbody

        index = Random.Range(1, 4);//get a random number from 1~3

        switch (index) // give it a different color according to its number
        {
            case 1:
                shipBody.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                shipBody.GetComponent<Renderer>().material.color = Color.red;
                GetTarget();
                break;
            case 3:
                shipBody.GetComponent<Renderer>().material.color = Color.yellow;
                player = GameObject.Find("Player");//access player gameobject
                randomPos = Random.Range(-Mathf.PI, Mathf.PI);//get a random pos because it will stay the orbit
                break;
        }


       

    }

    // Update is called once per frame
    void Update()
    {

        switch (index)
        {
            case 1:
                var step = speed * Time.deltaTime; 
                transform.position = Vector3.MoveTowards(transform.position, earthPos, step);//move toward the earth

                if (transform.position.x < -15 || transform.position.x > 15 || transform.position.y > 9 || transform.position.y < -9)
                {
                    Destroy(gameObject); // if it moves out, destroy it
                }

                break;

            case 2:
                
                if (Vector3.Distance(transform.position, target) < 0.01f)//if it reach its target
                {
                    timer += Time.deltaTime;

                    if(timer >= timeGap) //shoot a laser every specific amount of time
                    {
                        Shoot();
                        timer = 0;
                    }

                    
                }
                else
                {
                    MoveIn();
                }

            break;

            case 3:
                if (onTrack)//if it stay on the orbit
                {
                    

                    float x = Mathf.Cos(player.GetComponent<PlayerMovement>().rotateSpeed + randomPos) * player.GetComponent<PlayerMovement>().xAmplitude; // use sin and cos to make the object rotate
                    float y = Mathf.Sin(player.GetComponent<PlayerMovement>().rotateSpeed + randomPos) * player.GetComponent<PlayerMovement>().yAmplitude;
                    float z = transform.position.z;
                    transform.position = new Vector3(x, y, z);

                    countDown -= Time.deltaTime;//start the countdown


                    gameObject.GetComponentInChildren<TextMeshPro>().text = Mathf.Round(countDown).ToString();//display the countdown text

                    if(countDown <= 0)
                    {
                        Explode();//explode when the countdown comes to 0
                    }

                    

                }
                else
                {
                    var step3 = speed * Time.deltaTime; 
                    transform.position = Vector3.MoveTowards(transform.position, earthPos, step3);// move toward to the earth
                }
               

            break;

        }




        
        
    }

    void GetTarget()
    {
        if (transform.position.x >= 11)//get a target according to which side of the screen it's on
        {
            target = new Vector3(Random.Range(3, 7.5f), Random.Range(-4, 4), 0);
        }

        if (transform.position.x <= -11)
        {
            target = new Vector3(Random.Range(-3, -7.5f), Random.Range(-4, 4), 0);
        }

        if (transform.position.y >= 7)
        {
            target = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(2, 4), 0);
        }

        if (transform.position.y <= -7)
        {
            target = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-2, -4), 0);
        }
    }

    void MoveIn()
    {
        var step = speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, target, step);//move toward its target
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);//spawn a bullet at its position
        bullet.transform.position = target;
    }


    void Explode()
    {
        GameObject explposion = Instantiate(explosionPrefab);//spawn the explosion prefab
        explposion.transform.position = gameObject.transform.position;//set its position to this gameobject's position
        Destroy(gameObject);//destroy it
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")//if it collides with the player
        {
            Destroy(gameObject);//destroy it
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Circle" && index == 3)//if it collide with the orbit
        {
            onTrack = true;//stay on it
        }

        if(other.gameObject.name == "Shield")//if it collide with the shield
        {
            Destroy(gameObject);//destroy it
        }

    }




}
