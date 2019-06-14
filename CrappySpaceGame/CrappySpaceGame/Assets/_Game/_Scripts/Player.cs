using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };
    public enum abilities { NONE, MINE }

    // Initialize the public variables
    public states playerState;
    public abilities playerAbility;

    public string moveHorPlayerAxis;
    public string moveVerPlayerAxis;

    public string moveHorHitboxAxis;
    public string moveVerHitboxAxis;

    public string shootLeftAxis;
    public string shootRightAxis;

    public string abilityLeftButton;
    public string abilityRightButton;

    public string swapButton;

    public float movementSpeed;

    public Rigidbody hitboxRb;

    public float maxHitboxScale;
    public float hitboxScaleMod;

    public GameObject projectile;
    public GameObject mine;

    public float shootCooldown;
    public float abilityCooldown;
    public float shieldCooldown;

    public float ammo;

    public float shootDistance;
    public float mineDistance;

    public float speedMod;

    public Transform projectileParent;
    public Transform model;

    public GameObject shield;
    public bool shieldActive;

    public float turnIntensity;

    public float hp;

    public GameObject gameOverText;

    public Text ammoText;
    public Text hpText;

    [HideInInspector]
    public float shieldAlarm;

    // Initialize the private variables
    Rigidbody rb;

    float shootAlarm;
    float abilityAlarm;

    // Run this code once at the start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        switch (playerState)
        {
            // The default player state
            case states.DEFAULT:
                Move(moveHorPlayerAxis, moveVerPlayerAxis, rb, movementSpeed); // Move the player
                Move(moveHorHitboxAxis, moveVerHitboxAxis, hitboxRb, movementSpeed); // Move the hitbox
                Enlarge(); // Enlarge the hitbox
                Shoot(); // Shoot a projectile
                Swap(); // Swap position with the hitbox
                Ability(); // Use the current player ability
                Turn(); // Turn the player model towards the current movement direction
                Shield(); // Enable & disable the player shield
                Die(); // Die when health reaches below zero
                break;
        }

        ammoText.text = "Mines: " + ammo;
        hpText.text = "Health: " + hp;
    }

    // Move an object based on axis input
    void Move(string axisHor, string axisVer, Rigidbody targetRb, float speed)
    {
        // Get the user input
        float hSpeed = Input.GetAxis(axisHor);
        float vSpeed = Input.GetAxis(axisVer);

        // Set the player velocity
        Vector3 vel = ((transform.forward * hSpeed) * speed) + ((transform.up * vSpeed) * speed);
        targetRb.velocity = vel;
    }

    // Enlarge the hitbox
    void Enlarge()
    {
        Transform hitboxTransform = hitboxRb.transform;
        float dist = Vector3.Distance(transform.position, hitboxTransform.position);

        float scale = maxHitboxScale - (dist * hitboxScaleMod);

        hitboxTransform.localScale = new Vector3(scale, scale, scale);
    }

    // Shoot a projectile
    void Shoot()
    {
        float shoot = (Input.GetAxis(shootLeftAxis) * -1) + Input.GetAxis(shootRightAxis);
        if (shoot != 0f && shootAlarm <= 0f)
        {
            int dir = Mathf.RoundToInt(Mathf.Sign(shoot));
            Vector3 position = transform.position + ((transform.forward * dir) * shootDistance);

            var obj = Instantiate(projectile, transform);
            obj.transform.position = position;
            obj.transform.parent = projectileParent;

            var script = obj.GetComponent<Projectile>();

            script.direction = dir;

            float speed = 0f;
            switch (dir)
            {
                case -1:
                    speed = Mathf.Clamp(rb.velocity.x, -Mathf.Infinity, 0f) * dir;
                    break;

                case 1:
                    speed = Mathf.Clamp(rb.velocity.x, 0f, Mathf.Infinity) * dir;
                    break;
            }

            model.localScale = new Vector3(model.localScale.x, model.localScale.y, dir);

            script.movementSpeed += speed * speedMod;

            shootAlarm = shootCooldown;
        }

        shootAlarm--;
    }

    // Swap position with the hitbox
    void Swap()
    {
        if (Input.GetButtonDown(swapButton))
        {
            Vector3 shipPosOld = transform.position;
            transform.position = hitboxRb.transform.position;
            hitboxRb.transform.position = shipPosOld;
        }
    }

    // Use the current player ability
    void Ability()
    {
        switch (playerAbility)
        {
            // Drop a mine
            case abilities.MINE:
                Mine();
                break;
        }
    }

    // Drop a mine
    void Mine()
    {
        float ability = (Input.GetAxis(abilityLeftButton) * -1) + Input.GetAxis(abilityRightButton);
        if (ability != 0f && abilityAlarm <= 0f && ammo >= 1)
        {
            int dir = Mathf.RoundToInt(Mathf.Sign(ability));
            Vector3 position = transform.position + ((transform.forward * dir) * mineDistance);

            var obj = Instantiate(mine, transform);
            obj.transform.position = position;
            obj.transform.parent = projectileParent;

            var script = obj.GetComponent<Projectile>();
            script.movementSpeed = 0f;

            model.localScale = new Vector3(model.localScale.x, model.localScale.y, dir);

            abilityAlarm = abilityCooldown;
            ammo--;
        }

        abilityAlarm--;
    }

    // Turn the player model towards the current movement direction
    void Turn()
    {
        model.rotation = Quaternion.Euler(Input.GetAxis(moveVerPlayerAxis) * turnIntensity, 90f, Input.GetAxis(moveHorPlayerAxis) * turnIntensity);
    }

    // Enable & disable the player shield
    void Shield()
    {
        if (shieldAlarm > 0f)
        {
            shieldActive = false;
            shield.SetActive(false);
        }
        else
        {
            shieldActive = true;
            shield.SetActive(true);
        }

        shieldAlarm--;
    }

    // Check for enemy collision
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !shieldActive)
        {
            hp--;
            Destroy(other.gameObject);
        }
    }

    // Die when health reaches zero
    void Die()
    {
        if (hp <= 0f)
        {
            gameOverText.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
