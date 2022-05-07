using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public int life;

    public bool lose;

    public GameObject gameOverStuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life<=0)
        {
            lose = true; // lose
            gameOverStuff.SetActive(true); // activate the game over text
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") // if collide with the enemy
        {
            life -= 1;
            Destroy(collision.gameObject); // destroy the enemy
        }
    }

}
