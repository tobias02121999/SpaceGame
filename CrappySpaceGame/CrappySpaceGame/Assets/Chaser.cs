using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };

    // Initialize the public variables
    public states enemyState;
    public Vector2 movementSpeed;
    public Transform hitbox;

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
        hitbox = GameObject.FindGameObjectWithTag("IkHaatMnLeven").transform;
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        switch (enemyState)
        {
            // The default enemy state
            case states.DEFAULT:
                enemy.Follow(hitbox, speed, rb);
                enemy.Die();
                break;
        }
    }
}
