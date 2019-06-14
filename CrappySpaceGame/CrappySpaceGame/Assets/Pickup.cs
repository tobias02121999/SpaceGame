using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Initialize the public enums
    public enum type { MINE };

    // Initialize the public variables
    public type pickupType;
    public float ammo;

    // Check for collision with the player
    void OnTriggerEnter(Collider other)
    {
        var script = other.GetComponent<Player>();

        switch (pickupType)
        {
            // The mines ability
            case type.MINE:
                script.playerAbility = Player.abilities.MINE;
                break;
        }

        script.ammo += ammo;
        Destroy(this.gameObject);
    }

}
