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

        if (currentState == State.enlarge)//set a minimum radius so it won't collide with the earth
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
            case State.normal://if it's normal state
                transform.localScale = originalScale;//set the scale to normal scale
                speedUp = 1;//set the speed to 1s
                break;
            case State.enlarge:
                transform.localScale = enlargeScale;//enlarge the moon
                specialPowerTime -= Time.deltaTime;//set a timer
                
                break;
            case State.speedUp:
                speedUp = 3;//speed up the moon
                specialPowerTime -= Time.deltaTime;
                break;
            case State.duplicate:
                duplicate.SetActive(true);//activate the clone moon
                specialPowerTime -= Time.deltaTime;
                break;
            case State.shield:
                shield.SetActive(true);//activate the shield
                Invoke("goBackToNormal", 2f);//deactivate it after 2 secs
                break;
        }

        if (specialPowerTime <= 0)
        {
            stateTransition(State.normal);//if time's up, go back to normal state
        }


    }



    public void stateTransition(State targetState)
    {
        currentState = targetState;//transit to the state you put in
        speedUp = 1;
        transform.localScale = originalScale;
        duplicate.SetActive(false);
        shield.SetActive(false);
        specialPowerTime = 8f;//set a timer
    }

    void goBackToNormal()//go back to normal state
    {
        currentState = State.normal;
        speedUp = 1;
        transform.localScale = originalScale;
        duplicate.SetActive(false);
        shield.SetActive(false);
        specialPowerTime = 8f;
    }




    float map(float value, float leftMin, float leftMax, float rightMin, float rightMax) //mapping function
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

}
