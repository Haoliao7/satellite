using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xAmplitude;
    public float yAmplitude;

    public float frequency;
    public float rotateSpeed;

    public GameObject range;

    public GameManager myManager;


    bool goBack;

    public enum State
    {
        normal,
        enlarge,
        duplicate,
        speedUp,
        shield
    }

    public State currentState;

    public float specialPowerTime;

    [Header("Enlarge")]
    public Vector3 enlargeScale;
    public Vector3 originalScale;
    public float enlargeTime;

    [Header("Duplicate")]
    public GameObject duplicate;
    public float duplicateTime;
    public bool isPlayer;

    [Header("Speed up")]
    public float speedUp;
    public float speedUpTime;

    [Header("Shield")]
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.normal;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition; // the position of mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);//change your mouse position from screen point to world point

        /*
        xAmplitude = map(mouseWorldPos.x, -9, 9, 0f, 8f);//map the range of mousePosX to the range between 0 and 8
        yAmplitude = map(mouseWorldPos.y, -4.8f, 4.8f, 0f, 4f);//map the range of mousePosY to the range between 0 and 4
        */
        xAmplitude = mouseWorldPos.x;
        yAmplitude = mouseWorldPos.y;

        if (currentState == State.enlarge)
        {
            if (xAmplitude <= 1.25f)
            {
                xAmplitude = 1.25f;
            }
            if (yAmplitude <= 1.25f)
            {
                yAmplitude = 1.25f;
            }
        }
        else
        {
            if (xAmplitude <= 0.5f)
            {
                xAmplitude = 0.5f;
            }
            if (yAmplitude <= 0.5f)
            {
                yAmplitude = 0.5f;
            }
        }


        

        rotateSpeed += frequency * Time.deltaTime; //rotate speed



        float x = Mathf.Cos(rotateSpeed) * xAmplitude; // use sin and cos to make the object rotate
        float y = Mathf.Sin(rotateSpeed) * yAmplitude;
        float z = 0;
        transform.position = new Vector3(x, y, z);
        range.transform.localScale = new Vector3(xAmplitude * 2f, yAmplitude * 2f, range.transform.localScale.z); // make this image as big as the orbit

        
        

        if (Input.GetMouseButton(0))
        {
            frequency = 3 * speedUp;//if pressing left button, speed up 
            goBack = false;
        }
        else if (Input.GetMouseButton(1))
        {
            frequency = -3 * speedUp; //if pressing the right button, go back 
            goBack = true;
        }
        else if (goBack == true)
        {
            frequency = -1 * speedUp;//if releasing, slow down
        }
        else
        {
            frequency = 1 * speedUp;//if releasing, slow down
        }



        //special items

        switch (currentState)
        {
            case State.normal:
                transform.localScale = originalScale;
                specialPowerTime = 8f;
                speedUp = 1;
                break;
            case State.enlarge:
                transform.localScale = enlargeScale;
                specialPowerTime -= Time.deltaTime;
                
                break;
            case State.speedUp:
                speedUp = 3;
                specialPowerTime -= Time.deltaTime;
                break;
            case State.duplicate:
                duplicate.SetActive(true);
                specialPowerTime -= Time.deltaTime;
                break;
            case State.shield:
                shield.SetActive(true);
                Invoke("goBackToNormal", 2f);
                break;
        }

        if (specialPowerTime <= 0)
        {
            stateTransition(State.normal);
        }


    }


    /*public void Enlarge()
    {
        transform.localScale = enlargeScale;
        
        Invoke("Shrink", enlargeTime);
    }*/


    public void stateTransition(State targetState)
    {
        currentState = targetState;
        speedUp = 1;
        transform.localScale = originalScale;
        duplicate.SetActive(false);
        shield.SetActive(false);
        specialPowerTime = 8f;
    }

    void goBackToNormal()
    {
        currentState = State.normal;
        speedUp = 1;
        transform.localScale = originalScale;
        duplicate.SetActive(false);
        shield.SetActive(false);
        specialPowerTime = 8f;
    }


    /*public void SetActivateDuplicate()
    {
        duplicate.SetActive(true);
        Invoke("DeactivateDuplicate", duplicateTime);
    }
    
    void DeactivateDuplicate()
    {
        duplicate.SetActive(false);
    }

    public void SpeedUp()
    {
        myManager.speedUp *= 3;
        Invoke("SlowDown", speedUpTime);
    }

    void SlowDown()
    {
        myManager.speedUp /= 3;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") // if collide with the enemy
        {

            //add score

            
        }
    }


    float map(float value, float leftMin, float leftMax, float rightMin, float rightMax) //mapping function
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

}
