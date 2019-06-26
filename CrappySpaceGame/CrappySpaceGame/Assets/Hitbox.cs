using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // Initialize the public variables
    public float hitCooldown;
    public GameObject orbRed;
    public GameObject orbBlue;
    public Player player;

    // Initialize the private variables
    [HideInInspector]
    public bool isActive;
    float hitAlarm;

    // Check for collision
    void OnTriggerEnter(Collider other)
    {
        if (isActive && other.tag != "Border")
        {
            hitAlarm = hitCooldown;
            player.shieldAlarm = player.shieldCooldown;
        }
    }

    // Run this code every single frame
    void Update()
    {
        if (hitAlarm > 0f)
        {
            isActive = false;
            orbRed.SetActive(true);
            orbBlue.SetActive(false);
        }
        else
        {
            isActive = true;
            orbRed.SetActive(false);
            orbBlue.SetActive(true);
        }

        hitAlarm--;
    }
}
