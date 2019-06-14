using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Initialize the public variables
    public float movementSpeed;
    public float damage = 1;

    [HideInInspector]
    public int direction = 1;

    // Initialize the private variables
    Rigidbody rb;

    // Run this code once at the start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        Move(movementSpeed, rb); // Move the projectile
    }

    // Move the projectile
    void Move(float speed, Rigidbody targetRb)
    {
        // Set the projectile velocity
        Vector3 vel = (transform.forward * (speed * direction));
        targetRb.velocity = vel;
    }

    // Check for collision
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            // Check for collision with the level border
            case "Border":
                Destroy(this.gameObject);
                break;

            case "Enemy":
                other.transform.parent.GetComponentInParent<Enemy>().hp -= damage;
                Destroy(this.gameObject);
                break;
        }
    }
}
