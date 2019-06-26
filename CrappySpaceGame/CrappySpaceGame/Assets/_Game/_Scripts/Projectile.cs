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

    public bool isFriendly;
    public bool isNeutral;

    GameObject player;

    // Initialize the private variables
    Rigidbody rb;

    // Run this code once at the start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        Move(movementSpeed, rb); // Move the projectile

        var dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= 50f)
            Destroy(this.gameObject);
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
                if (isFriendly || isNeutral)
                {
                    other.transform.parent.GetComponentInParent<Enemy>().hp -= damage;
                    Destroy(this.gameObject);
                }
                break;

            case "GodHelpMij":
                if (!isFriendly || isNeutral)
                {
                    var script = other.GetComponent<Player>();
                    if (!script.shieldActive)
                    {
                        script.hp--;
                        Destroy(this.gameObject);
                    }
                }
                break;
        }
    }
}
