using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Initialize the public variables
    public Vector2 spawnAlarmCooldown;

    public GameObject inceptor;
    public GameObject chaser;

    public Transform entityParent;

    public float progressionRate;

    public Text scoreText;

    public Vector2 chaserChance;

    // Initialize the private variables
    float spawnAlarm;
    float progress;

    Vector2 spawnAreaX;
    Vector2 spawnAreaY;

    GameObject obj;
    
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
            var rand = Mathf.RoundToInt(Random.Range(chaserChance.x, chaserChance.y));
            if (rand == 0)
                obj = Instantiate(chaser, transform);
            else
                obj = Instantiate(inceptor, transform);

            obj.transform.position = new Vector3(Random.Range(spawnAreaX.x, spawnAreaX.y), Random.Range(spawnAreaY.x, spawnAreaY.y), 0f);
            obj.transform.parent = entityParent;
            obj.transform.localScale = new Vector3(1f, 1f, 1f);

            spawnAlarm = Mathf.Clamp(spawnAlarmCooldown.y - progress, spawnAlarmCooldown.x, Mathf.Infinity);
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
            scoreText.text = "Score: " + Mathf.RoundToInt(progress);
    }
}
