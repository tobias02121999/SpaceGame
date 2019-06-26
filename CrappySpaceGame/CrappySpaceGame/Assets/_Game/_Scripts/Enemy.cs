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
    float shootAlarm;

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
    public void Die(bool dropMine)
    {
        if (hp <= 0f)
        {
            if (spawnMine && dropMine)
            {
                var obj = Instantiate(mine, transform);
                obj.transform.parent = null;
            }

            Destroy(this.gameObject);
        }
    }

    // Follow the hitbox
    public void Follow(Transform target1, Transform target2, float speed, Rigidbody targetRb)
    {
        targetRb.velocity = transform.forward * speed;

        Transform target;
        if (target1.GetComponent<Hitbox>().isActive)
            target = target1;
        else
            target = target2;

        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 10f, 0f);

        transform.rotation = Quaternion.LookRotation(newDir);

        var dist = Vector3.Distance(transform.position, target.position);

        if (dist <= .1f)
            Destroy(this.gameObject);
    }

    // Shoot a projectile
    public void Shoot(GameObject projectile, int dir, int shootCooldown, float distance, bool isMine)
    {
        if (shootAlarm <= 0f)
        {
            var obj = Instantiate(projectile, transform);

            Vector3 position = transform.position + ((transform.forward * dir) * distance);
            obj.transform.position = position;

            obj.transform.parent = null;

            var script = obj.GetComponent<Projectile>();

            script.direction = dir;

            if (isMine)
                script.movementSpeed = 0f;

            shootAlarm = shootCooldown;
        }
        else
            shootAlarm--;
    }
}
