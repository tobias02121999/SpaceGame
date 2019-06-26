using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };

    // Initialize the public variables
    public states enemyState;
    public Vector2 movementSpeed;
    public int movementDir;

    // Initialize the private variables
    Rigidbody rb;
    Enemy enemy;
    float speed;

    // Run this code once at the start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
        speed = Random.Range(movementSpeed.x, movementSpeed.y);
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        switch (enemyState)
        {
            // The default enemy state
            case states.DEFAULT:
                enemy.Move(speed, movementDir, rb);
                enemy.Die(false);
                break;
        }
    }
}
