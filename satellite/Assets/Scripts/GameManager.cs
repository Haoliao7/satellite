using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float timeGap;
    public GameObject enemyPrefab;
    public GameObject itemPrefab;

    int index;

    float time;
    public Text timeText;
    public Text lifeText;
    public GameObject earth;
    public GameObject player;
    //public float speedUp;

    void Start()
    {
        Invoke("SpawnNewEnemy", 1f); // wait 1 sec to let the player be familiar with the mechanic

        InvokeRepeating("SpawnSpecialItems", 10f,10f); // spawn a special item every 8 secs
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false; // not display the cursor

        if (earth.GetComponent<Earth>().lose == false)
        {
            time += Time.deltaTime; //if you lose, stop the time
        }

        lifeText.text = "Life : " + earth.GetComponent<Earth>().life;
        timeText.text = "Time : " + Mathf.Round(time).ToString(); //display the time
    }

    void SpawnNewEnemy()
    {
        timeGap = Random.Range(1f, 2.5f); //randomize the time gap
        index = Random.Range(1, 5); //get a int number between 1~4

        GameObject enemy = Instantiate(enemyPrefab); // instantiate a new enemy

        switch (index)
        {
            case 1://put it at the left side of the screen
                enemy.transform.position = new Vector3(
                    -12,
                    Random.Range(-4.2f, 4.2f), //but randomize its y pos
                    0
                    );
                break;
            case 2://put it at the right side of the screen
                enemy.transform.position = new Vector3(
                    12,
                    Random.Range(-4.2f, 4.2f), //but randomize its y pos
                    0
                    );
                break;
            case 3://put it above the screen
                enemy.transform.position = new Vector3(
                    Random.Range(-7.5f, 7.5f),//but randomize its x pos
                    8, 
                    0
                    );
                break;
            case 4://put it below the screen
                enemy.transform.position = new Vector3(
                    Random.Range(-7.5f, 7.5f),//but randomize its x pos
                    -8,
                    0
                    );
                break;
        }

        
        if(earth.GetComponent<Earth>().lose == false)
        {
            Invoke("SpawnNewEnemy", timeGap);//spawn a new enemy after a certain amount of time
        }

        
    }

    void SpawnSpecialItems()
    {
        GameObject newItem = Instantiate(itemPrefab); // instantiate a new item
        index = Random.Range(1, 5); //get a int number between 1~4

        switch (index)
        {
            case 1://put it at the left side of the screen
                newItem.transform.position = new Vector3(
                    -12,
                    Random.Range(-4.2f, 4.2f), //but randomize its y pos
                    0
                    );
                break;
            case 2://put it at the right side of the screen
                newItem.transform.position = new Vector3(
                    12,
                    Random.Range(-4.2f, 4.2f), //but randomize its y pos
                    0
                    );
                break;
            case 3://put it above the screen
                newItem.transform.position = new Vector3(
                    Random.Range(-7.5f, 7.5f),//but randomize its x pos
                    8,
                    0
                    );
                break;
            case 4://put it below the screen
                newItem.transform.position = new Vector3(
                    Random.Range(-7.5f, 7.5f),//but randomize its x pos
                    -8,
                    0
                    );
                break;
        }



        //newItem.transform.position = new Vector3(Random.Range(5, -5), Random.Range(3, -3), 0);
    }



    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu"); //load menu scene
    }


}
