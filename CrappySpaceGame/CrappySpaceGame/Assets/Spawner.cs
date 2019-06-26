using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Initialize the public variables
    public Vector2 spawnAlarmCooldown;

    public GameObject asteroid;
    public GameObject inceptor;
    public GameObject chaser;
    public GameObject bomber;

    public Transform entityParent;
    public GameObject victoryText;

    public float progressionRate;

    public Text scoreText;

    // Initialize the private variables
    float spawnAlarm;
    float progress;

    Vector2 spawnAreaX;
    Vector2 spawnAreaY;

    GameObject obj;

    public int level = 1;
    int rand;
    
    // Run this code once at the start
    void Start()
    {
        spawnAreaX.x = transform.position.x - (transform.localScale.x / 2f);
        spawnAreaX.y = transform.position.x + (transform.localScale.x / 2f);

        spawnAreaY.x = transform.position.y - (transform.localScale.y / 2f);
        spawnAreaY.y = transform.position.y + (transform.localScale.y / 2f);
    }

    // Run this code every single frame
    void Update()
    {
        progress += progressionRate;
        spawnAlarm--;

        if (spawnAlarm <= 0f)
        {
            switch (level)
            {
                case 1:
                    obj = Instantiate(asteroid, transform);
                    break;

                case 2:
                    rand = Mathf.RoundToInt(Random.Range(0, 1.49f));
                    if (rand == 0)
                        obj = Instantiate(asteroid, transform);
                    else
                        obj = Instantiate(inceptor, transform);
                    break;

                case 3:
                    rand = Mathf.RoundToInt(Random.Range(0, 1.49f));
                    if (rand == 0)
                        obj = Instantiate(inceptor, transform);
                    if (rand == 1)
                        obj = Instantiate(asteroid, transform);
                    break;

                case 4:
                    rand = Mathf.RoundToInt(Random.Range(0, 2.49f));
                    if (rand == 0)
                        obj = Instantiate(chaser, transform);
                    if (rand == 1)
                        obj = Instantiate(inceptor, transform);
                    if (rand == 2)
                        obj = Instantiate(asteroid, transform);
                    break;

                case 5:
                    rand = Mathf.RoundToInt(Random.Range(0, 3.49f));
                    if (rand == 0)
                        obj = Instantiate(chaser, transform);
                    if (rand == 1)
                        obj = Instantiate(inceptor, transform);
                    if (rand == 2)
                        obj = Instantiate(asteroid, transform);
                    if (rand == 3)
                        obj = Instantiate(bomber, transform);
                    break;

                case 6:
                    entityParent.gameObject.SetActive(false);
                    victoryText.SetActive(true);
                    break;
            }

            obj.transform.position = new Vector3(Random.Range(spawnAreaX.x, spawnAreaX.y), Random.Range(spawnAreaY.x, spawnAreaY.y), 0f);
            obj.transform.parent = entityParent;
            obj.transform.localScale = new Vector3(1f, 1f, 1f);

            spawnAlarm = Mathf.Clamp(spawnAlarmCooldown.y - progress, spawnAlarmCooldown.x, Mathf.Infinity) / 2f;
        }

        if (progress >= 850)
        {
            level++;
            progress = 0;
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
            scoreText.text = "Level: " + level;
    }
}
