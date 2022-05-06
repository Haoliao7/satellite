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
        rb = GetComponent<Rigidbody>();

        index = Random.Range(1, 4);

        switch (index)
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
                player = GameObject.Find("Player");
                randomPos = Random.Range(-Mathf.PI, Mathf.PI);
                break;
        }


        //Invoke("StartMoving", timeBeforeMoving);//wait a certain amount of time and start moving

    }

    // Update is called once per frame
    void Update()
    {

        /*if (transform.position.x >= 11 || transform.position.x <= -11)//if it's at the left or right side of the screen
        {
            warningSquare.GetComponent<Transform>().localScale = new Vector3(scaling, 1, 1);//enlarge the width of the warning rectangle
        }


        if (transform.position.y >= 7 || transform.position.y <= -7)//if it's at the top or bottom side of the screen
        {
            warningSquare.GetComponent<Transform>().localScale = new Vector3(1, scaling, 1);//enlarge the height of the warning rectangle
        }

        if (scaling <=50)
        {
            scaling += Time.deltaTime * scalingSpeed;//if it reachs 50, stop scaling
            
        }*/

        switch (index)
        {
            case 1:
                var step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, earthPos, step);

                if (transform.position.x < -15 || transform.position.x > 15 || transform.position.y > 9 || transform.position.y < -9)
                {
                    Destroy(gameObject); // if it moves out, destroy it
                }

                break;

            case 2:
                
                if (Vector3.Distance(transform.position, target) < 0.01f)
                {
                    timer += Time.deltaTime;

                    if(timer >= timeGap)
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
                if (onTrack)
                {
                    

                    float x = Mathf.Cos(player.GetComponent<PlayerMovement>().rotateSpeed + randomPos) * player.GetComponent<PlayerMovement>().xAmplitude; // use sin and cos to make the object rotate
                    float y = Mathf.Sin(player.GetComponent<PlayerMovement>().rotateSpeed + randomPos) * player.GetComponent<PlayerMovement>().yAmplitude;
                    float z = transform.position.z;
                    transform.position = new Vector3(x, y, z);

                    countDown -= Time.deltaTime;


                    gameObject.GetComponentInChildren<TextMeshPro>().text = Mathf.Round(countDown).ToString();

                    if(countDown <= 0)
                    {
                        Explode();
                    }

                    

                }
                else
                {
                    var step3 = speed * Time.deltaTime; // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, earthPos, step3);
                }
               

            break;

        }




        
        
    }

    void GetTarget()
    {
        if (transform.position.x >= 11)
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
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = target;
    }


    void Explode()
    {
        GameObject explposion = Instantiate(explosionPrefab);
        explposion.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

       
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Circle" && index == 3)
        {
            onTrack = true;
        }

        if(other.gameObject.name == "Shield")
        {
            Destroy(gameObject);
        }

    }




}
