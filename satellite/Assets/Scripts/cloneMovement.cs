using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloneMovement : MonoBehaviour
{
    public float xAmplitude;
    public float yAmplitude;

    public float frequency;
    public float rotateSpeed;

    bool goBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition; // the position of mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);//change your mouse position from screen point to world point

        xAmplitude = mouseWorldPos.x;
        yAmplitude = mouseWorldPos.y;

        if (xAmplitude <= 0.5f)
        {
            xAmplitude = 0.5f;
        }
        if (yAmplitude <= 0.5f)
        {
            yAmplitude = 0.5f;
        }

        rotateSpeed += frequency * Time.deltaTime; //rotate speed

        float x = Mathf.Cos(rotateSpeed + Mathf.PI) * xAmplitude; // use sin and cos to make the object rotate
        float y = Mathf.Sin(rotateSpeed + Mathf.PI) * yAmplitude;
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z);

        if (Input.GetMouseButton(0))
        {
            frequency = 3  ;//if pressing left button, speed up 
            goBack = false;
        }
        else if (Input.GetMouseButton(1))
        {
            frequency = -3  ; //if pressing the right button, go back 
            goBack = true;
        }
        else if (goBack == true)
        {
            frequency = -1  ;//if releasing, slow down
        }
        else
        {
            frequency = 1 ;//if releasing, slow down
        }

    }
}
