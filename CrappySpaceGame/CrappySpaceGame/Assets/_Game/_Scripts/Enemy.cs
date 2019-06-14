using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Initialize the public variables
    public float hp;
    public Vector2 mineSpawnChance;
    public GameObject mine;

    // Initialize the private variables
    bool spawnMine;

    // Run this code once at the start
    void Start()
    {
        spawnMine = (Mathf.RoundToInt(Random.Range(mineSpawnChance.x, mineSpawnChance.y)) == 0);
    }

    // Move the enemy
    public void Move(float speed, int dir, Rigidbody targetRb)
    {
        // Set the enemy velocity
        Vector3 vel = (transform.forward * (speed * dir));
        targetRb.velocity = vel;
    }

    // Die if hp reaches below zero
    public void Die()
    {
        if (hp <= 0f)
        {
            if (spawnMine)
            {
                var obj = Instantiate(mine, transform);
                obj.transform.parent = null;
            }

            Destroy(this.gameObject);
        }
    }

    // Follow the hitbox
    public void Follow(Transform target, float speed, Rigidbody targetRb)
    {
        targetRb.velocity = transform.forward * speed;

        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 10f, 0f);

        transform.rotation = Quaternion.LookRotation(newDir);

        var dist = Vector3.Distance(transform.position, target.position);

        if (dist <= .1f)
            Destroy(this.gameObject);
    }
}
